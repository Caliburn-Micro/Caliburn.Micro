namespace Caliburn.Micro
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Linq;

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
        /// <returns>The dialog result.</returns>
        bool? ShowDialog(object rootModel, object context = null);

        /// <summary>
        /// Shows a window for the specified model.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="context">The context.</param>
        void Show(object rootModel, object context = null);
    }

    /// <summary>
    /// A service that manages windows.
    /// </summary>
    public class WindowManager : IWindowManager
    {
        static readonly DependencyProperty IsElementGeneratedProperty =
            DependencyProperty.RegisterAttached(
                "IsElementGenerated",
                typeof(bool),
                typeof(WindowManager),
                new PropertyMetadata(false, null)
                );

        /// <summary>
        /// Used to retrieve the root, non-framework-created view.
        /// </summary>
        /// <param name="view">The view to search.</param>
        /// <returns>The root element that was not created by the framework.</returns>
        /// <remarks>In certain instances the WindowManager creates UI elements in order to display windows.
        /// For example, if you ask the window manager to show a UserControl as a dialog, it creates a window to host the UserControl in.
        /// The WindowManager marks that element as a framework-created element so that it can determine what it created vs. what was intended by the developer.
        /// Calling GetSignificantView allows the framework to discover what the original element was. 
        /// </remarks>
        public static DependencyObject GetSignificantView(DependencyObject view)
        {
            if ((bool)view.GetValue(IsElementGeneratedProperty))
                return (DependencyObject)((ContentControl)view).Content;

            return view;
        }

        /// <summary>
        /// Shows a modal dialog for the specified model.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="context">The context.</param>
        /// <returns>The dialog result.</returns>
        public bool? ShowDialog(object rootModel, object context = null)
        {
            return CreateWindow(rootModel, true, context).ShowDialog();
        }

        /// <summary>
        /// Shows a window for the specified model.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="context">The context.</param>
        public void Show(object rootModel, object context = null)
        {
            CreateWindow(rootModel, false, context).Show();
        }

        Window CreateWindow(object rootModel, bool isDialog, object context)
        {
            var view = EnsureWindow(rootModel, ViewLocator.LocateForModel(rootModel, null, context), isDialog);
            ViewModelBinder.Bind(rootModel, view, context);

            var haveDisplayName = rootModel as IHaveDisplayName;
            if (haveDisplayName != null && !ConventionManager.HasBinding(view, Window.TitleProperty))
            {
                var binding = new Binding("DisplayName") { Mode = BindingMode.TwoWay };
                view.SetBinding(Window.TitleProperty, binding);
            }

            new WindowConductor(rootModel, view);

            return view;
        }

        protected virtual Window EnsureWindow(object model, object view, bool isDialog)
        {
            var window = view as Window;

            if (window == null) {
                window = new Window {
                    Content = view,
                    SizeToContent = SizeToContent.WidthAndHeight
                };

                window.SetValue(IsElementGeneratedProperty, true);

                var owner = InferOwnerOf(window);
                if (owner != null)
                {
                    window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    window.Owner = owner;
                }
                else
                {
                    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }
            }
            else
            {
                var owner = InferOwnerOf(window);
                if (owner != null && isDialog)
                    window.Owner = owner;
            }

            return window;
        }

        protected virtual Window InferOwnerOf(Window window)
        {
            if(Application.Current == null)
                return null;

            var active = Application.Current.Windows.OfType<Window>()
                .Where(x => x.IsActive)
                .FirstOrDefault();
            active = active ?? Application.Current.MainWindow;
            return active == window ? null : active;
        }

        class WindowConductor
        {
            bool deactivatingFromView;
            bool deactivateFromViewModel;
            bool actuallyClosing;
            readonly Window view;
            readonly object model;

            public WindowConductor(object model, Window view)
            {
                this.model = model;
                this.view = view;

                var activatable = model as IActivate;
                if (activatable != null)
                    activatable.Activate();

                var deactivatable = model as IDeactivate;
                if (deactivatable != null)
                {
                    view.Closed += Closed;
                    deactivatable.Deactivated += Deactivated;
                }

                var guard = model as IGuardClose;
                if (guard != null)
                    view.Closing += Closing;
            }

            void Closed(object sender, EventArgs e)
            {
                view.Closed -= Closed;
                view.Closing -= Closing;

                if (deactivateFromViewModel)
                    return;

                var deactivatable = (IDeactivate)model;

                deactivatingFromView = true;
                deactivatable.Deactivate(true);
                deactivatingFromView = false;
            }

            void Deactivated(object sender, DeactivationEventArgs e)
            {
                ((IDeactivate)model).Deactivated -= Deactivated;

                if (!e.WasClosed || deactivatingFromView)
                    return;

                deactivateFromViewModel = true;
                actuallyClosing = true;
                view.Close();
                actuallyClosing = false;
                deactivateFromViewModel = false;
            }

            void Closing(object sender, CancelEventArgs e)
            {
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