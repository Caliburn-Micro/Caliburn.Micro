﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Caliburn.Micro
{
    /// <summary>
    ///   A basic implementation of <see cref="INavigationService" /> designed to adapt the <see cref="Frame" /> control.
    /// </summary>
    public class FrameAdapter : INavigationService, IDisposable
    {
        readonly Frame frame;
        readonly bool treatViewAsLoaded;
        event NavigatingCancelEventHandler ExternalNavigatingHandler = delegate { };

        /// <summary>
        ///   Creates an instance of <see cref="FrameAdapter" />
        /// </summary>
        /// <param name="frame"> The frame to represent as a <see cref="INavigationService" /> . </param>
        /// <param name="treatViewAsLoaded"> Tells the frame adapter to assume that the view has already been loaded by the time OnNavigated is called. This is necessary when using the TransitionFrame. </param>
        public FrameAdapter(Frame frame, bool treatViewAsLoaded = false)
        {
            this.frame = frame;
            this.treatViewAsLoaded = treatViewAsLoaded;
            this.frame.Navigating += OnNavigating;
            this.frame.Navigated += OnNavigated;
        }

        /// <summary>
        ///   Occurs before navigation
        /// </summary>
        /// <param name="sender"> The event sender. </param>
        /// <param name="e"> The event args. </param>
        protected virtual async void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            ExternalNavigatingHandler(sender, e);
            if (e.Cancel)
            {
                return;
            }

            var fe = frame.Content as FrameworkElement;
            if (fe == null)
            {
                return;
            }

            if (fe.DataContext is IGuardClose guard)
            {
                var canClose = await guard.CanCloseAsync(CancellationToken.None);

                if (!canClose)
                {
                    e.Cancel = true;
                    return;
                }
            }

            var deactivator = fe.DataContext as IDeactivate;

            // If we are navigating to the same page there is no need to deactivate
            // e.g. When the app is activated with Fast Switch
            if (deactivator != null && frame.CurrentSource != e.Uri)
            {
                await deactivator.DeactivateAsync(CanCloseOnNavigating(sender, e), CancellationToken.None);
            }
        }

        /// <summary>
        /// Called to check whether or not to close current instance on navigating.
        /// </summary>
        /// <param name="sender"> The event sender from OnNavigating event. </param>
        /// <param name="e"> The event args from OnNavigating event. </param>
        protected virtual bool CanCloseOnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            return false;
        }

        /// <summary>
        ///   Occurs after navigation
        /// </summary>
        /// <param name="sender"> The event sender. </param>
        /// <param name="e"> The event args. </param>
        protected virtual async void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (e.Uri.IsAbsoluteUri || e.Content == null)
            {
                return;
            }

            ViewLocator.InitializeComponent(e.Content);

            var viewModel = ViewModelLocator.LocateForView(e.Content);
            if (viewModel == null)
            {
                return;
            }

            var page = e.Content as Page;
            if (page == null)
            {
                throw new ArgumentException("View '" + e.Content.GetType().FullName + "' should inherit from Page or one of its descendents.");
            }

            if (treatViewAsLoaded)
            {
                page.SetValue(View.IsLoadedProperty, true);
            }

            TryInjectParameters(viewModel, e.ExtraData);
            ViewModelBinder.Bind(viewModel, page, null);

            if (viewModel is IActivate activator)
            {
                await activator.ActivateAsync();
            }

        }

        /// <summary>
        ///   Attempts to inject query string parameters from the view into the view model.
        /// </summary>
        /// <param name="viewModel"> The view model.</param>
        /// <param name="parameter"> The parameter.</param>
        protected virtual void TryInjectParameters(object viewModel, object parameter)
        {
            var viewModelType = viewModel.GetType();

            var dictionaryParameter = parameter as IDictionary<string, object>;

            if (dictionaryParameter != null)
            {
                foreach (var pair in dictionaryParameter)
                {
                    var property = viewModelType.GetPropertyCaseInsensitive(pair.Key);

                    if (property == null)
                    {
                        continue;
                    }

                    property.SetValue(viewModel, MessageBinder.CoerceValue(property.PropertyType, pair.Value, null), null);
                }
            }
            else
            {
                var property = viewModelType.GetPropertyCaseInsensitive("Parameter");

                if (property == null)
                    return;

                property.SetValue(viewModel, MessageBinder.CoerceValue(property.PropertyType, parameter, null), null);
            }
        }

        /// <summary>
        ///   The <see cref="Uri" /> source.
        /// </summary>
        public Uri Source
        {
            get { return frame.Source; }
            set { frame.Source = value; }
        }

        /// <summary>
        ///   Indicates whether the navigator can navigate back.
        /// </summary>
        public bool CanGoBack
        {
            get { return frame.CanGoBack; }
        }

        /// <summary>
        ///   Indicates whether the navigator can navigate forward.
        /// </summary>
        public bool CanGoForward
        {
            get { return frame.CanGoForward; }
        }

        /// <summary>
        ///   The current <see cref="Uri" /> source.
        /// </summary>
        public Uri CurrentSource
        {
            get { return frame.CurrentSource; }
        }

        /// <summary>
        ///   The current content.
        /// </summary>
        public object CurrentContent
        {
            get { return frame.Content; }
        }

        /// <summary>
        ///   Stops the loading process.
        /// </summary>
        public void StopLoading()
        {
            frame.StopLoading();
        }

        /// <summary>
        ///   Navigates back.
        /// </summary>
        public void GoBack()
        {
            frame.GoBack();
        }

        /// <summary>
        ///   Navigates forward.
        /// </summary>
        public void GoForward()
        {
            frame.GoForward();
        }

        /// <inheritdoc />
        public void NavigateToViewModel(Type viewModel, object extraData = null)
        {
            var viewType = ViewLocator.LocateTypeForModelType(viewModel, null, null);

            var packUri = ViewLocator.DeterminePackUriFromType(viewModel, viewType);

            var uri = new Uri(packUri, UriKind.Relative);

            frame.Navigate(uri, extraData);
        }

        /// <inheritdoc />
        public void NavigateToViewModel<TViewModel>(object extraData = null)
        {
            NavigateToViewModel(typeof(TViewModel), extraData);
        }

        /// <summary>
        ///   Removes the most recent entry from the back stack.
        /// </summary>
        /// <returns> The entry that was removed. </returns>
        public JournalEntry RemoveBackEntry()
        {
            return frame.RemoveBackEntry();
        }

        /// <summary>
        /// Disposes the FrameAdapter instance, detaching event handlers to prevent memory leaks.
        /// </summary>
        public void Dispose()
        {
            this.frame.Navigating -= OnNavigating;
            this.frame.Navigated -= OnNavigated;
        }

        /// <summary>
        ///   Raised after navigation.
        /// </summary>
        public event NavigatedEventHandler Navigated
        {
            add { frame.Navigated += value; }
            remove { frame.Navigated -= value; }
        }

        /// <summary>
        ///   Raised prior to navigation.
        /// </summary>
        public event NavigatingCancelEventHandler Navigating
        {
            add { ExternalNavigatingHandler += value; }
            remove { ExternalNavigatingHandler -= value; }
        }

        /// <summary>
        ///   Raised when navigation fails.
        /// </summary>
        public event NavigationFailedEventHandler NavigationFailed
        {
            add { frame.NavigationFailed += value; }
            remove { frame.NavigationFailed -= value; }
        }

        /// <summary>
        ///   Raised when navigation is stopped.
        /// </summary>
        public event NavigationStoppedEventHandler NavigationStopped
        {
            add { frame.NavigationStopped += value; }
            remove { frame.NavigationStopped -= value; }
        }

        /// <summary>
        ///   Raised when a fragment navigation occurs.
        /// </summary>
        public event FragmentNavigationEventHandler FragmentNavigation
        {
            add { frame.FragmentNavigation += value; }
            remove { frame.FragmentNavigation -= value; }
        }
    }
}
