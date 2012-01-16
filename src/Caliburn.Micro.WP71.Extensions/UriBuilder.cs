namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Builds a Uri in a strongly typed fashion, based on a ViewModel.
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public class UriBuilder<TViewModel> {
        readonly Dictionary<string, string> queryString = new Dictionary<string, string>();
        INavigationService navigationService;

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
            var uri = BuildUri();

            if (navigationService == null) {
                throw new Exception("Cannot navigate without attaching an INavigationService. Call AttachTo first.");
            }

            navigationService.Navigate(uri);
        }

        /// <summary>
        /// Builds the URI.
        /// </summary>
        /// <returns>A uri constructed with the current configuration information.</returns>
        public Uri BuildUri() {
            var viewType = ViewLocator.LocateTypeForModelType(typeof(TViewModel), null, null);
            if(viewType == null) {
                throw new Exception(string.Format("No view was found for {0}. See the log for searched views.", typeof(TViewModel).FullName));
            }

            var packUri = ViewLocator.DeterminePackUriFromType(typeof(TViewModel), viewType);
            var qs = BuildQueryString();

            return new Uri(packUri + qs, UriKind.Relative);
        }

        string BuildQueryString() {
            if (queryString.Count < 1) {
                return string.Empty;
            }

            var result = queryString
                .Aggregate("?", (current, pair) => current + (pair.Key + "=" + pair.Value + "&"));

            return result.Remove(result.Length - 1);
        }
    }
}