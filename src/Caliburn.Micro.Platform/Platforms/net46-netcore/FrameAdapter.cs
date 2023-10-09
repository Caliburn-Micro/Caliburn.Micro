using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Caliburn.Micro {
    /// <summary>
    ///   A basic implementation of <see cref="INavigationService" /> designed to adapt the <see cref="Frame" /> control.
    /// </summary>
    public class FrameAdapter : INavigationService, IDisposable {
        private readonly Frame _frame;
        private readonly bool _treatViewAsLoaded;

        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameAdapter"/> class.
        /// </summary>
        /// <param name="frame"> The frame to represent as a <see cref="INavigationService" /> . </param>
        /// <param name="treatViewAsLoaded"> Tells the frame adapter to assume that the view has already been loaded by the time OnNavigated is called. This is necessary when using the TransitionFrame. </param>
        public FrameAdapter(Frame frame, bool treatViewAsLoaded = false) {
            _frame = frame;
            _treatViewAsLoaded = treatViewAsLoaded;
            _frame.Navigating += OnNavigating;
            _frame.Navigated += OnNavigated;
        }

        /// <summary>
        ///   Raised after navigation.
        /// </summary>
        public event NavigatedEventHandler Navigated {
            add => _frame.Navigated += value;
            remove => _frame.Navigated -= value;
        }

        /// <summary>
        ///   Raised prior to navigation.
        /// </summary>
        public event NavigatingCancelEventHandler Navigating {
            add => ExternalNavigatingHandler += value;
            remove => ExternalNavigatingHandler -= value;
        }

        /// <summary>
        ///   Raised when navigation fails.
        /// </summary>
        public event NavigationFailedEventHandler NavigationFailed {
            add => _frame.NavigationFailed += value;
            remove => _frame.NavigationFailed -= value;
        }

        /// <summary>
        ///   Raised when navigation is stopped.
        /// </summary>
        public event NavigationStoppedEventHandler NavigationStopped {
            add => _frame.NavigationStopped += value;
            remove => _frame.NavigationStopped -= value;
        }

        /// <summary>
        ///   Raised when a fragment navigation occurs.
        /// </summary>
        public event FragmentNavigationEventHandler FragmentNavigation {
            add => _frame.FragmentNavigation += value;
            remove => _frame.FragmentNavigation -= value;
        }

        private event NavigatingCancelEventHandler ExternalNavigatingHandler
            = (sender, e) => { };

        /// <summary>
        ///   Gets a value indicating whether the navigator can navigate back.
        /// </summary>
        public bool CanGoBack
            => _frame.CanGoBack;

        /// <summary>
        ///   Gets a value indicating whether the navigator can navigate forward.
        /// </summary>
        public bool CanGoForward
            => _frame.CanGoForward;

        /// <summary>
        ///   Gets the current <see cref="Uri" /> source.
        /// </summary>
        public Uri CurrentSource
            => _frame.CurrentSource;

        /// <summary>
        ///   Gets the current content.
        /// </summary>
        public object CurrentContent
            => _frame.Content;

        /// <summary>
        ///   Gets or sets the <see cref="Uri" /> source.
        /// </summary>
        public Uri Source {
            get => _frame.Source;
            set => _frame.Source = value;
        }

        /// <summary>
        ///   Stops the loading process.
        /// </summary>
        public void StopLoading()
            => _frame.StopLoading();

        /// <summary>
        ///   Navigates back.
        /// </summary>
        public void GoBack()
            => _frame.GoBack();

        /// <summary>
        ///   Navigates forward.
        /// </summary>
        public void GoForward()
            => _frame.GoForward();

        /// <summary>
        ///   Removes the most recent entry from the back stack.
        /// </summary>
        /// <returns> The entry that was removed. </returns>
        public JournalEntry RemoveBackEntry()
            => _frame.RemoveBackEntry();

        /// <inheritdoc />
        public void NavigateToViewModel<TViewModel>(object extraData = null)
            => NavigateToViewModel(typeof(TViewModel), extraData);

        /// <inheritdoc />
        public void NavigateToViewModel(Type viewModel, object extraData = null) {
            Type viewType = ViewLocator.LocateTypeForModelType(viewModel, null, null);

            string packUri = ViewLocator.DeterminePackUriFromType(viewModel, viewType);

            var uri = new Uri(packUri, UriKind.Relative);

            _frame.Navigate(uri, extraData);
        }

        /// <summary>
        /// Dispose managed resources.
        /// </summary>
        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Called to check whether or not to close current instance on navigating.
        /// </summary>
        /// <param name="sender"> The event sender from OnNavigating event. </param>
        /// <param name="e"> The event args from OnNavigating event. </param>
        protected virtual bool CanCloseOnNavigating(object sender, NavigatingCancelEventArgs e)
            => false;

        /// <summary>
        ///   Occurs before navigation.
        /// </summary>
        /// <param name="sender"> The event sender. </param>
        /// <param name="e"> The event args. </param>
        protected virtual async void OnNavigating(object sender, NavigatingCancelEventArgs e) {
            ExternalNavigatingHandler(sender, e);
            if (e.Cancel) {
                return;
            }

            if (_frame.Content is not FrameworkElement fe) {
                return;
            }

            if (fe.DataContext is IGuardClose guard) {
                bool canClose = await guard.CanCloseAsync(CancellationToken.None);

                if (!canClose) {
                    e.Cancel = true;
                    return;
                }
            }

            // If we are navigating to the same page there is no need to deactivate
            // e.g. When the app is activated with Fast Switch
            if (fe.DataContext is IDeactivate deactivator && _frame.CurrentSource != e.Uri) {
                await deactivator.DeactivateAsync(CanCloseOnNavigating(sender, e), CancellationToken.None);
            }
        }

        /// <summary>
        ///   Occurs after navigation.
        /// </summary>
        /// <param name="sender"> The event sender. </param>
        /// <param name="e"> The event args. </param>
        protected virtual async void OnNavigated(object sender, NavigationEventArgs e) {
            if (e.Uri.IsAbsoluteUri || e.Content == null) {
                return;
            }

            ViewLocator.InitializeComponent(e.Content);

            object viewModel = ViewModelLocator.LocateForView(e.Content);
            if (viewModel == null) {
                return;
            }

            if (e.Content is not Page page) {
                throw new ArgumentException("View '" + e.Content.GetType().FullName + "' should inherit from Page or one of its descendents.");
            }

            if (_treatViewAsLoaded) {
                page.SetValue(View.IsLoadedProperty, true);
            }

            TryInjectParameters(viewModel, e.ExtraData);
            ViewModelBinder.Bind(viewModel, page, null);

            if (viewModel is IActivate activator) {
                await activator.ActivateAsync();
            }

            GC.Collect();
        }

        /// <summary>
        ///   Attempts to inject query string parameters from the view into the view model.
        /// </summary>
        /// <param name="viewModel"> The view model.</param>
        /// <param name="parameter"> The parameter.</param>
        protected virtual void TryInjectParameters(object viewModel, object parameter) {
            Type viewModelType = viewModel.GetType();

            if (parameter is IDictionary<string, object> dictionaryParameter) {
                foreach (KeyValuePair<string, object> pair in dictionaryParameter) {
                    System.Reflection.PropertyInfo property = viewModelType.GetPropertyCaseInsensitive(pair.Key);

                    if (property == null) {
                        continue;
                    }

                    property.SetValue(viewModel, MessageBinder.CoerceValue(property.PropertyType, pair.Value, null), null);
                }
            } else {
                System.Reflection.PropertyInfo property = viewModelType.GetPropertyCaseInsensitive("Parameter");

                if (property == null) {
                    return;
                }

                property.SetValue(viewModel, MessageBinder.CoerceValue(property.PropertyType, parameter, null), null);
            }
        }

        /// <summary>
        /// Dispose used resources.
        /// </summary>
        /// <param name="disposing">
        /// Value indicating whether or not to dispose managed resources.
        /// Can be false to dispose unmanaged resources only
        /// and to set large fields to null without calling 'Dispose' on managed resources.
        /// </param>
        protected virtual void Dispose(bool disposing) {
            if (_isDisposed) {
                return;
            }

            if (disposing) {
                // dispose managed state (managed objects)
                _frame.Navigating -= OnNavigating;
                _frame.Navigated -= OnNavigated;
            }

            // free unmanaged resources (unmanaged objects) and override finalizer
            // set large fields to null
            _isDisposed = true;
        }
    }
}
