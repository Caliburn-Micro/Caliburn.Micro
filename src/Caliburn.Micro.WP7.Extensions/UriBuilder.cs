namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;

    public class UriBuilder<TViewModel> {
        readonly Dictionary<string, string> queryString = new Dictionary<string, string>();
        readonly string entryAssemblyName;
        INavigationService navigationService;

        public UriBuilder() {
            entryAssemblyName = GetAssemblyName(Application.Current.GetType().Assembly);
        }

        public UriBuilder<TViewModel> WithParam<TValue>(Expression<Func<TViewModel, TValue>> property, TValue value) {
            queryString[property.GetMemberInfo().Name] = value.ToString();
            return this;
        }

        public UriBuilder<TViewModel> AttachTo(INavigationService navigationService) {
            this.navigationService = navigationService;
            return this;
        }

        public void Navigate() {
            var pageName = DeterminePageName();
            var qs = BuildQueryString();
            var uri = new Uri(pageName + qs, UriKind.Relative);

            if(navigationService == null) {
                throw new Exception("Cannot navigate without attaching an INavigationService. Call AttachTo first.");
            }

            navigationService.Navigate(uri);
        }

        string DeterminePageName() {
            var page = ViewLocator.LocateTypeForModelType(typeof(TViewModel), null, null);
            var pageAssemblyName = GetAssemblyName(page.Assembly);
            var pageName = page.FullName.Replace(pageAssemblyName, "").Replace(".", "/") + ".xaml";

            if(!entryAssemblyName.Equals(pageAssemblyName)) {
                return "/" + pageAssemblyName + ";component" + pageName;
            }

            return pageName;
        }

        string BuildQueryString() {
            if(queryString.Count < 1) {
                return string.Empty;
            }

            var result = queryString
                .Aggregate("?", (current, pair) => current + (pair.Key + "=" + pair.Value + "&"));

            return result.Remove(result.Length - 1);
        }

        static string GetAssemblyName(Assembly assembly) {
            return assembly.FullName.Remove(assembly.FullName.IndexOf(","));
        }
    }
}