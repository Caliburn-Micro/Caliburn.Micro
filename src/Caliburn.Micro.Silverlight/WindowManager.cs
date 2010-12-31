namespace Caliburn.Micro
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>
    /// A service that manages windows.
    /// </summary>
    public interface IWindowManager
    {
        /// <summary>
        /// Shows a modal dialog for the specified model.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="context">The context.</param>
        void ShowDialog(object rootModel, object context = null);

        /// <summary>
        /// Shows a toast notification for the specified model.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="durationInMilliseconds">How long the notification should appear for.</param>
        /// <param name="context">The context.</param>
        void ShowNotification(object rootModel, int durationInMilliseconds, object context = null);
    }

    /// <summary>
    /// A service that manages windows.
    /// </summary>
    public class WindowManager : IWindowManager
    {
        /// <summary>
        /// Shows a modal dialog for the specified model.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="context">The context.</param>
        public virtual void ShowDialog(object rootModel, object context = null)
        {
            var view = EnsureWindow(rootModel, ViewLocator.LocateForModel(rootModel, null, context));
            ViewModelBinder.Bind(rootModel, view, context);

            var haveDisplayName = rootModel as IHaveDisplayName;
            if (haveDisplayName != null && !ConventionManager.HasBinding(view, ChildWindow.TitleProperty))
            {
                var binding = new Binding("DisplayName") { Mode = BindingMode.TwoWay };
                view.SetBinding(ChildWindow.TitleProperty, binding);
            }

            new WindowConductor(rootModel, view);

            view.Show();
        }

        /// <summary>
        /// Shows a toast notification for the specified model.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="durationInMilliseconds">How long the notification should appear for.</param>
        /// <param name="context">The context.</param>
        public virtual void ShowNotification(object rootModel, int durationInMilliseconds, object context = null)
        {
            var window = new NotificationWindow();
            var view = ViewLocator.LocateForModel(rootModel, window, null);

            ViewModelBinder.Bind(rootModel, view, null);
            window.Content = (FrameworkElement)view;

            var activator = rootModel as IActivate;
            if (activator != null)
                activator.Activate();

            var deactivator = rootModel as IDeactivate;
            if(deactivator != null) {
                EventHandler handler = null;
                handler = delegate {
                    window.Closed -= handler;
                    deactivator.Deactivate(true);
                };
                window.Closed += handler;
            }

            window.Show(durationInMilliseconds);
        }

        protected virtual ChildWindow EnsureWindow(object model, object view)
        {
            var window = view as ChildWindow;

            if(window == null)
            {
                window = new ChildWindow { Content = view };
                window.SetValue(View.IsGeneratedProperty, true);
            }

            return window;
        }

        class WindowConductor {
            bool deactivatingFromView;
            bool deactivateFromViewModel;
            bool actuallyClosing;
            readonly ChildWindow view;
            readonly object model;

            public WindowConductor(object model, ChildWindow view) {
                this.model = model;
                this.view = view;

                var activatable = model as IActivate;
                if (activatable != null)
                    activatable.Activate();

                var deactivatable = model as IDeactivate;
                if (deactivatable != null) {
                    view.Closed += Closed;
                    deactivatable.Deactivated += Deactivated;
                }

                var guard = model as IGuardClose;
                if (guard != null)
                    view.Closing += Closing;
            }

            void Closed(object sender, EventArgs e) {
                view.Closed -= Closed;
                view.Closing -= Closing;

                if (deactivateFromViewModel)
                    return;

                var deactivatable = (IDeactivate)model;

                deactivatingFromView = true;
                deactivatable.Deactivate(true);
                deactivatingFromView = false;
            }

            void Deactivated(object sender, DeactivationEventArgs e) {
                ((IDeactivate)model).Deactivated -= Deactivated;

                if(!e.WasClosed || deactivatingFromView)
                    return;

                deactivateFromViewModel = true;
                actuallyClosing = true;
                view.Close();
                actuallyClosing = false;
                deactivateFromViewModel = false;
            }

            void Closing(object sender, CancelEventArgs e) {
                if (e.Cancel)
                    return;

                var guard = (IGuardClose)model;

                if (actuallyClosing)
                {
                    actuallyClosing = false;
                    return;
                }

                bool runningAsync = false, shouldEnd = false;

                guard.CanClose(canClose => {
                    Execute.OnUIThread(() => {
                        if(runningAsync && canClose) {
                            actuallyClosing = true;
                            view.Close();
                        }
                        else e.Cancel = !canClose;

                        shouldEnd = true;
                    });
                });

                if (shouldEnd)
                    return;

                runningAsync = e.Cancel = true;
            }
        }
    }
}