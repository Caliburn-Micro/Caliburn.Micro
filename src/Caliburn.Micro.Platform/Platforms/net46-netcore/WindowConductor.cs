using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Caliburn.Micro
{
    public class WindowConductor
    {
        private bool deactivatingFromView;
        private bool deactivateFromViewModel;
        private bool actuallyClosing;
        private readonly Window view;
        private readonly object model;

        public WindowConductor(object model, Window view)
        {
            this.model = model;
            this.view = view;
        }

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

            if (model is IGuardClose guard)
            {
                view.Closing += Closing;
            }
        }

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
