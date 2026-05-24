using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Caliburn.Micro
{
    /// <summary>
    /// Manages the lifecycle of a window and its associated view model.
    /// </summary>
    public class WindowConductor
    {
        private bool deactivatingFromView;
        private bool deactivateFromViewModel;
        private bool actuallyClosing;
        private readonly Window view;
        private readonly object model;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowConductor"/> class.
        /// </summary>
        /// <param name="model">The view model associated with the window.</param>
        /// <param name="view">The window being managed.</param>
        public WindowConductor(object model, Window view)
        {
            this.model = model;
            this.view = view;
        }

        /// <summary>
        /// Initializes the conductor asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
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
        /// Handles the window's Closed event.
        /// </summary>
        private async void Closed(object sender, EventArgs e)
        {
            view.Closed -= Closed;
            view.Closing -= Closing;

            if (deactivateFromViewModel)
            {
                return;
            }

            var deactivatable = (IDeactivate)model;

            deactivatingFromView = true;
            await deactivatable.DeactivateAsync(true);
            deactivatingFromView = false;
        }

        /// <summary>
        /// Handles the view model's Deactivated event.
        /// </summary>
        private Task Deactivated(object sender, DeactivationEventArgs e)
        {
            if (!e.WasClosed)
            {
                return Task.FromResult(false);
            }

            ((IDeactivate)model).Deactivated -= Deactivated;

            if (deactivatingFromView)
            {
                return Task.FromResult(true);
            }

            deactivateFromViewModel = true;
            actuallyClosing = true;
            view.Close();
            actuallyClosing = false;
            deactivateFromViewModel = false;

            return Task.FromResult(true);
        }

        /// <summary>
        /// Handles the window's Closing event.
        /// </summary>
        private async void Closing(object sender, CancelEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            var guard = (IGuardClose)model;

            if (actuallyClosing)
            {
                actuallyClosing = false;
                return;
            }

            var cachedDialogResult = view.DialogResult;

            e.Cancel = true;

            await Task.Yield();

            var canClose = await guard.CanCloseAsync(CancellationToken.None);

            if (!canClose)
                return;

            actuallyClosing = true;

            if (cachedDialogResult == null)
            {
                view.Close();
            }
            else if (view.DialogResult != cachedDialogResult)
            {
                view.DialogResult = cachedDialogResult;
            }
        }
    }
}
