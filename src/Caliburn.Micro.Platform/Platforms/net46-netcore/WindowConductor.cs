using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Caliburn.Micro {
    /// <summary>
    /// Window conductor to manage Closing and Deactivation of view model.
    /// Guard closing event if the view model implements <see cref="IGuardClose"/>.
    /// </summary>
    public class WindowConductor {
        private readonly Window _view;
        private readonly object _model;

        private bool deactivatingFromView;
        private bool deactivateFromViewModel;
        private bool actuallyClosing;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowConductor"/> class.
        /// </summary>
        /// <param name="model">The view model.</param>
        /// <param name="view">The window.</param>
        public WindowConductor(object model, Window view) {
            _model = model;
            _view = view;
        }

        /// <summary>
        /// Activate the view model and subscribe to closing and closed events.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task InitialiseAsync() {
            await ActivateViewModel();
            SubscribeToCloseEvetns();
            GuardClosing();
        }

        private async Task ActivateViewModel() {
            if (_model is not IActivate activator) {
                return;
            }

            await activator.ActivateAsync();
        }

        private void SubscribeToCloseEvetns() {
            if (_model is not IDeactivate deactivatable) {
                return;
            }

            _view.Closed += Closed;
            deactivatable.Deactivated += Deactivated;
        }

        private void GuardClosing() {
            if (_model is not IGuardClose guard) {
                return;
            }

            _view.Closing += Closing;
        }

        private async void Closed(object sender, EventArgs e) {
            _view.Closed -= Closed;
            _view.Closing -= Closing;

            if (deactivateFromViewModel ||
                _model is not IDeactivate deactivatable) {
                return;
            }

            deactivatingFromView = true;
            await deactivatable.DeactivateAsync(true);
            deactivatingFromView = false;
        }

        private Task Deactivated(object sender, DeactivationEventArgs e) {
            if (!e.WasClosed) {
                return Task.FromResult(false);
            }

            ((IDeactivate)_model).Deactivated -= Deactivated;

            if (deactivatingFromView) {
                return Task.FromResult(true);
            }

            deactivateFromViewModel = true;
            actuallyClosing = true;
            _view.Close();
            actuallyClosing = false;
            deactivateFromViewModel = false;

            return Task.FromResult(true);
        }

        private async void Closing(object sender, CancelEventArgs e) {
            if (e.Cancel) {
                return;
            }

            if (actuallyClosing) {
                actuallyClosing = false;

                return;
            }

            e.Cancel = true;

            await Task.Yield();

            var guard = (IGuardClose)_model;
            if (!await guard.CanCloseAsync(CancellationToken.None)) {
                return;
            }

            actuallyClosing = true;

            bool? cachedDialogResult = _view.DialogResult;
            if (cachedDialogResult == null) {
                _view.Close();

                return;
            }

            if (_view.DialogResult == cachedDialogResult) {
                return;
            }

            _view.DialogResult = cachedDialogResult;
        }
    }
}
