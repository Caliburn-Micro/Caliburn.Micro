namespace Caliburn.Micro.Xamarin.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Builds a Uri in a strongly typed fashion, based on a ViewModel.
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public class NavigateHelper<TViewModel>
    {
        readonly Dictionary<string, object> parameters = new Dictionary<string, object>();
        INavigationService navigationService;

        /// <summary>
        /// Adds a query string parameter to the Uri.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="value">The property value.</param>
        /// <returns>Itself</returns>
        public NavigateHelper<TViewModel> WithParam<TValue>(Expression<Func<TViewModel, TValue>> property, TValue value)
        {
            if (value is ValueType || !ReferenceEquals(null, value))
            {
                parameters[property.GetMemberInfo().Name] = value;
            }

            return this;
        }

        /// <summary>
        /// Attaches a navigation servies to this builder.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <returns>Itself</returns>
        public NavigateHelper<TViewModel> AttachTo(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            return this;
        }

        /// <summary>
        /// Navigates to the Uri represented by this builder.
        /// </summary>
        public void Navigate(bool animated = true)
        {
            if (navigationService == null)
            {
                throw new InvalidOperationException("Cannot navigate without attaching an INavigationService. Call AttachTo first.");
            }

            navigationService.NavigateToViewModelAsync<TViewModel>(parameters, animated);
        }
    }
}
