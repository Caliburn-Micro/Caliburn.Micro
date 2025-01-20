using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Caliburn.Micro
{
    /// <summary>
    /// Represents a frame that supports navigation to view models.
    /// </summary>
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class NavigationFrame : ContentControl, INavigationService
    {
        private static readonly ILog Log = LogManager.GetLog(typeof(NavigationFrame));

        private string defaultContent { get; } = "Default Content";
        private readonly List<object> navigationStack = new List<object>();
        private int navigationStackIndex = 0;
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationFrame"/> class.
        /// </summary>
        public NavigationFrame()
        {
            Content = defaultContent;
            this.Loaded += NavigationFrame_Loaded;
            ContentProperty.Changed.AddClassHandler<NavigationFrame>((sender, e) => NavigationFrame_ContentChanged(sender, e));
        }

        private void NavigationFrame_Loaded(object sender, RoutedEventArgs e)
        {
            Log.Info("Navigation Frame loaded");
            OnNavigationServiceReady(new EventArgs());
        }

        private void NavigationFrame_ContentChanged(NavigationFrame sender, AvaloniaPropertyChangedEventArgs e)
        {
            Log.Info("Content changed");
            Log.Info($"Content {Tag}");
            Tag = null;
        }

        private async void NavigationFrame_LayoutUpdated(object sender, EventArgs e)
        {
            Log.Info("LayoutUpdated");
            var control = sender as UserControl;
            if (control != null)
            {
                Log.Info("Control is not null");
                var viewModel = control.DataContext;
                if (viewModel != null && viewModel is IActivate activator)
                {
                    Log.Info("Activating view model");
                    await activator.ActivateAsync();
                }
            }
        }

        private async void NavigationFrame_TransitionCompleted(object sender, TransitionCompletedEventArgs e)
        {
            Log.Info("Transition completed");
            var control = e.To as UserControl;
            if (!object.ReferenceEquals(e.To, e.From))
            {
                Log.Info("Control is not null");
                var viewModel = control?.DataContext;
                if (viewModel != null && viewModel is IActivate activator)
                {
                    Log.Info("Activating view model");
                    await activator.ActivateAsync();
                }
            }
        }

        /// <summary>
        /// Occurs when the navigation service is ready.
        /// </summary>
        public event EventHandler NavigationServiceReady;

        /// <summary>
        /// Raises the <see cref="NavigationServiceReady"/> event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected virtual void OnNavigationServiceReady(EventArgs e)
        {
            EventHandler handler = this.NavigationServiceReady;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        /// <summary>
        /// Navigates to the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model to navigate to.</param>
        private void NavigateToViewModel(object viewModel, bool addToStack = true)
        {
            if (viewModel == null)
            {
                Log.Info("ViewModel is null. Falling back to default content.");
                Content = defaultContent;
                return;
            }

            var viewInstance = ViewLocator.LocateForModel(viewModel, null, null);
            if (viewInstance == null)
            {
                Log.Warn($"Couldn't find view for '{viewModel}'. Is it registered? Falling back to default content.");
                Content = defaultContent;
                return;
            }

            ViewModelBinder.Bind(viewModel, viewInstance, null);
            Log.Info($"View Model {viewModel}");
            Tag = "Navigating";
            viewInstance.DataContext = viewModel;
            Content = viewInstance;
            if (addToStack)
            {
                navigationStack.Add(viewModel);
                navigationStackIndex = navigationStack.Count;
            }
        }

        /// <inheritdoc/>
        public Task GoBackAsync(bool animated = true)
        {
            Log.Info("Going back");
            if (navigationStackIndex > 0)
                navigationStackIndex--;

            NavigateToViewModel(navigationStack[navigationStackIndex - 1], false);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task NavigateToViewAsync(Type viewType, object parameter = null, bool animated = true)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task NavigateToViewAsync<T>(object parameter = null, bool animated = true)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task NavigateToViewModelAsync(Type viewModelType, object parameter = null, bool animated = true)
        {
            var vm = Caliburn.Micro.IoC.GetInstance(viewModelType, null);
            TryInjectParameters(vm, parameter);
            NavigateToViewModel(vm);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets or sets the ViewModel.
        /// </summary>
        public static string ViewModel
        {
            get; set;
        }

        /// <inheritdoc/>
        public Task NavigateToViewModelAsync<T>(object parameter = null, bool animated = true)
        {
            Log.Info($"Navigate to View model type {typeof(T)}");
            Log.Info($"Navigate to View model parameter not null {parameter != null}");
            return NavigateToViewModelAsync(typeof(T), parameter, animated);
        }

        /// <inheritdoc/>
        public Task NavigateToViewModelAsync(Screen viewModel, object parameter = null, bool animated = true)
        {
            Log.Info("Navigate to a screen");
            NavigateToViewModel(viewModel);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task GoBackToRootAsync(bool animated = true)
        {
            Log.Info("Going back to root");
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a string that represents the current object for debugging.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        private string GetDebuggerDisplay()
        {
            return $"{nameof(NavigationFrame)}: Content={Content}, IsNavigationServiceReady={NavigationServiceReady != null}";
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
    }
}
