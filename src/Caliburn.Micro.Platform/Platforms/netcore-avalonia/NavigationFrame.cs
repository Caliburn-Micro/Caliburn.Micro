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

        /// <summary>
        /// Gets the default content displayed when no view model is set.
        /// </summary>
        private string defaultContent { get; } = "Default Content";

        /// <summary>
        /// The navigation stack holding the view models.
        /// </summary>
        private readonly List<object> navigationStack = new List<object>();

        /// <summary>
        /// The current index in the navigation stack.
        /// </summary>
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

        /// <summary>
        /// Handles the Loaded event of the NavigationFrame.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private async void NavigationFrame_Loaded(object sender, RoutedEventArgs e)
        {
            Log.Info("Navigation Frame loaded");
            await ActivateItemHelper(sender);
            OnNavigationServiceReady(new EventArgs());
        }

        private static async Task ActivateItemHelper(object sender)
        {
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

        /// <summary>
        /// Handles the Content changed event of the NavigationFrame.
        /// </summary>
        /// <param name="sender">The NavigationFrame instance.</param>
        /// <param name="e">The event data.</param>
        private async void NavigationFrame_ContentChanged(NavigationFrame sender, AvaloniaPropertyChangedEventArgs e)
        {
            Log.Info("Content changed");
            Log.Info($"Content {Tag}");
            await ActivateItemHelper(e.NewValue);
            Tag = null;
        }

        /// <summary>
        /// Handles the LayoutUpdated event of the NavigationFrame.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private async void NavigationFrame_LayoutUpdated(object sender, EventArgs e)
        {
            Log.Info("LayoutUpdated");
            await ActivateItemHelper(sender);
        }

        /// <summary>
        /// Handles the TransitionCompleted event of the NavigationFrame.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private async void NavigationFrame_TransitionCompleted(object sender, TransitionCompletedEventArgs e)
        {
            Log.Info("Transition completed");
            await ActivateItemHelper(e.To);
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
        /// <param name="addToStack">Whether to add the view model to the navigation stack.</param>
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
                navigationStackIndex = navigationStack.Count - 1;
            }
        }

        /// <inheritdoc/>
        public Task GoBackAsync(bool animated = true)
        {
            Log.Info("Going back");
            if (navigationStackIndex > 0)
                navigationStackIndex--;

            NavigateToViewModel(navigationStack[navigationStackIndex], false);
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

        /// <summary>
        /// Navigates to the specified view model type.
        /// </summary>
        /// <param name="viewModelType">The type of the view model.</param>
        /// <param name="parameter">The parameter to pass to the view model.</param>
        /// <param name="animated">Whether to animate the transition.</param>
        /// <returns>A completed task.</returns>
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
        /// Attempts to inject query string parameters from the view into the view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="parameter">The parameter.</param>
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
