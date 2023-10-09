﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Caliburn.Micro {
    /// <summary>
    /// Builds a Uri in a strongly typed fashion, based on a ViewModel.
    /// </summary>
    /// <typeparam name="TViewModel">The ViewModel type.</typeparam>
    public class NavigationHelper<TViewModel> {
        private readonly Dictionary<string, object> queryString = new Dictionary<string, object>();
        private INavigationService navigationService;

        /// <summary>
        /// Adds a query string parameter to the Uri.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="value">The property value.</param>
        /// <returns>Itself.</returns>
        public NavigationHelper<TViewModel> WithParam<TValue>(Expression<Func<TViewModel, TValue>> property, TValue value) {
            queryString[property.GetMemberInfo().Name] = value;

            return this;
        }

        /// <summary>
        /// Attaches a navigation servies to this builder.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <returns>Itself.</returns>
        public NavigationHelper<TViewModel> AttachTo(INavigationService navigationService) {
            this.navigationService = navigationService;

            return this;
        }

        /// <summary>
        /// Navigates to the Uri represented by this builder.
        /// </summary>
        public void Navigate() {
            if (navigationService == null) {
                throw new InvalidOperationException("Cannot navigate without attaching an INavigationService. Call AttachTo first.");
            }

            navigationService.NavigateToViewModel<TViewModel>(queryString);
        }
    }
}
