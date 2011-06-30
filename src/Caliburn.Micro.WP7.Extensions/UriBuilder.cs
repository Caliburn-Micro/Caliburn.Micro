namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;

    /// <summary>
    /// Builds a Uri in a strongly typed fashion, based on a ViewModel.
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public class UriBuilder<TViewModel> {
        readonly Dictionary<string, string> queryString = new Dictionary<string, string>();
        readonly string entryAssemblyName;
        INavigationService navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UriBuilder&lt;TViewModel&gt;"/> class.
        /// </summary>
        public UriBuilder() {
            entryAssemblyName = GetAssemblyName(Application.Current.GetType().Assembly);
        }

        /// <summary>
        /// Adds a query string parameter to the Uri.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="value">The property value.</param>
        /// <returns>Itself</returns>
        public UriBuilder<TViewModel> WithParam<TValue>(Expression<Func<TViewModel, TValue>> property, TValue value) {
            if (!Equals(default(TValue), value)) {
                queryString[property.GetMemberInfo().Name] = value.ToString();
            }

            return this;
        }

        /// <summary>
        /// Attaches a navigation servies to this builder.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <returns>Itself</returns>
        public UriBuilder<TViewModel> AttachTo(INavigationService navigationService) {
            this.navigationService = navigationService;
            return this;
        }

        /// <summary>
        /// Navigates to the Uri represented by this builder.
        /// </summary>
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
            if(page == null) {
                throw new Exception(string.Format("No view was found for {0}. See the log for searched views.", typeof(TViewModel).FullName));
            }

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