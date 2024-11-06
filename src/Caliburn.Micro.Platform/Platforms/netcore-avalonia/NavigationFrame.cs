using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;

namespace Caliburn.Micro
{
    /// <summary>
    /// Represents a frame that supports navigation to view models.
    /// </summary>
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class NavigationFrame : TransitioningContentControl, IStyleable, INavigationService
    {
        private static readonly ILog Log = LogManager.GetLog(typeof(NavigationFrame));

        private string defaultContent { get; } = "Default Content";

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationFrame"/> class.
        /// </summary>
        public NavigationFrame()
        {
            Content = defaultContent;
            this.AttachedToVisualTree += NavigationFrame_AttachedToVisualTree;
        }

        /// <summary>
        /// Handles the event when the frame is attached to the visual tree.
        /// </summary>
        private void NavigationFrame_AttachedToVisualTree(object sender, VisualTreeAttachmentEventArgs e)
        {
            OnNavigationServiceReady(new EventArgs());
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

        Type IStyleable.StyleKey => typeof(TransitioningContentControl);

        /// <summary>
        /// Navigates to the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model to navigate to.</param>
        private void NavigateToViewModel(object? viewModel)
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
            Log.Info($"View {viewInstance}");
            viewInstance.DataContext = viewModel;
            Content = viewInstance;
        }

        /// <inheritdoc/>
        public Task GoBackAsync(bool animated = true)
        {
            throw new NotImplementedException();
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
            Log.Info($"View model type {viewModelType}");
            var vm = Caliburn.Micro.IoC.GetInstance(viewModelType, null);
            Log.Info($"VM is null {vm == null}");
            NavigateToViewModel(vm);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task NavigateToViewModelAsync<T>(object parameter = null, bool animated = true)
        {
            throw new NotImplementedException();
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
    }
}
