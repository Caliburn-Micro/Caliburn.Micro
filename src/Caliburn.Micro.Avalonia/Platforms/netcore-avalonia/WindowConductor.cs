
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.LogicalTree;

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
                /*
                                             
                Using window.Closed here leads to strange DataContext issues in some cases, causing the 
                application to hang after closing the main window.
                
                Example case: 
                Closing a Conductor.Collection.OneActive window having it's children displayed in a TabControl.

                When TabControl.Items cleared, ContentPresenter.ContentChanged works differently depending on 
                whether it is attached to a logical tree. If it is attached, it sets the parent of the old child 
                (the closed tab) to null. This also sets DataContext and View.Model to null.

                Problems begin in the Closed handler because the ContentPresenter is no longer connected to a 
                logical tree. In this case it sets the inheritance parent of the child control to it's parent. 
                This sets DataContext to an inherited value instead of null, changing View.Model of the child 
                control to it's parent screen.

                */
                //view.Closed += Closed;
                deactivatable.Deactivated += Deactivated;
            }

            if (model is IGuardClose guard)
            {
                view.Closing += Closing;
            }
        }

        private async void Closed(object sender, EventArgs e)
        {
            //view.Closed -= Closed;
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
            
            // calling Closed() here instead of adding it to view.Close
            Closed(null, null);

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

            //var cachedDialogResult = view.DialogResult;

            e.Cancel = true;

            await Task.Yield();

            var canClose = await guard.CanCloseAsync(CancellationToken.None);

            if (!canClose)
                return;

            actuallyClosing = true;

            //if (cachedDialogResult == null)
            //{
                view.Close();
            //}
            //else if (view.DialogResult != cachedDialogResult)
            //{
            //  view.DialogResult = cachedDialogResult;
            //}
        }
    }
}
