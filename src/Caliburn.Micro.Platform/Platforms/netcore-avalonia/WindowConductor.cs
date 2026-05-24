using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;

namespace Caliburn.Micro
{
    /// <summary>
    /// Manages the lifecycle and interactions between a window and its view model.
    /// </summary>
    public class WindowConductor
    {
        private bool _deactivatingFromView;
        private bool _deactivateFromViewModel;
        private bool _actuallyClosing;
        private readonly Window view;
        private readonly object model;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowConductor"/> class.
        /// </summary>
        /// <param name="model">The view model associated with the window.</param>
        /// <param name="view">The window view to be managed by the conductor.</param>
        public WindowConductor(object model, Window view)
        {
            this.model = model;
            this.view = view;
        }

        /// <summary>
        /// Initializes the WindowConductor asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous initialization operation.</returns>
        public async Task InitialiseAsync()
        {
            if (model is IActivate activator)
            {
                await activator.ActivateAsync();
            }

            if (model is IDeactivate deactivatable)
            {
                view.Closed += Closed;
                deactivatable.Deactivated += Deactivated;
            }

            if (model is IGuardClose)
            {
                view.Closing += Closing;
            }
        }

        /// <summary>
        /// Handles the window closed event.
        /// </summary>
        private async void Closed(object sender, EventArgs e)
        {
            view.Closed -= Closed;
            view.Closing -= Closing;

            if (_deactivateFromViewModel)
            {
                return;
            }

            var deactivatable = (IDeactivate)model;

            _deactivatingFromView = true;
            await deactivatable.DeactivateAsync(true);
            _deactivatingFromView = false;
        }

        /// <summary>
        /// Handles the view model deactivated event.
        /// </summary>
        private Task<bool> Deactivated(object sender, DeactivationEventArgs e)
        {
            if (!e.WasClosed)
            {
                return Task.FromResult(false);
            }

            ((IDeactivate)model).Deactivated -= Deactivated;

            if (_deactivatingFromView)
            {
                return Task.FromResult(true);
            }

            _deactivateFromViewModel = true;
            _actuallyClosing = true;
            view.Close();
            _actuallyClosing = false;
            _deactivateFromViewModel = false;

            return Task.FromResult(true);
        }

        /// <summary>
        /// Handles the window closing event.
        /// </summary>
        private void Closing(object sender, CancelEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            if (_actuallyClosing)
            {
                _actuallyClosing = false;
                return;
            }

            e.Cancel = true;

            _ = Dispatcher.UIThread.InvokeAsync(async () =>
            {
                var canClose = await ((IGuardClose)model).CanCloseAsync(CancellationToken.None);

                if (!canClose)
                    return;

                _actuallyClosing = true;
                view.Close();
                // On macOS a crash occurs when view.Close() is called after a suspension with DispatcherPriority higher than Input.
            }, DispatcherPriority.Input);
        }
    }
}
