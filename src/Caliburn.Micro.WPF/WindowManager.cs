namespace Caliburn.Micro
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public interface IWindowManager
    {
        bool? ShowDialog(object rootModel, object context = null);
        void Show(object rootModel, object context = null);
    }

    public class WindowManager : IWindowManager
    {
        static readonly DependencyProperty IsElementGeneratedProperty =
            DependencyProperty.RegisterAttached(
                "IsElementGenerated",
                typeof(bool),
                typeof(WindowManager),
                new PropertyMetadata(false, null)
                );

        bool actuallyClosing;

        internal static DependencyObject GetSignificantView(DependencyObject view)
        {
            if ((bool)view.GetValue(IsElementGeneratedProperty))
                return (DependencyObject)((ContentControl)view).Content;

            return view;
        }

        public bool? ShowDialog(object rootModel, object context = null)
        {
            return CreateWindow(rootModel, true, context).ShowDialog();
        }

        public void Show(object rootModel, object context = null)
        {
            CreateWindow(rootModel, false, context).Show();
        }

        Window CreateWindow(object rootModel, bool isDialog, object context)
        {
            var view = EnsureWindow(rootModel, ViewLocator.LocateForModel(rootModel, context), isDialog);
            ViewModelBinder.Bind(rootModel, view, context);

            var activatable = rootModel as IActivate;
            if (activatable != null)
                activatable.Activate();

            var deactivatable = rootModel as IDeactivate;
            if (deactivatable != null)
                view.Closed += (s, e) => deactivatable.Deactivate(true);

            var guard = rootModel as IGuardClose;
            if (guard != null)
                view.Closing += (s, e) => OnShutdownAttempted(guard, view, e);

            return view;
        }

        static Window EnsureWindow(object model, object view, bool isDialog)
        {
            var window = view as Window;

            if (window == null)
            {
                window = new Window {
                    Content = view,
                    SizeToContent = SizeToContent.WidthAndHeight
                };

                window.SetValue(IsElementGeneratedProperty, true);

                if (Application.Current != null
                   && Application.Current.MainWindow != null
                    && Application.Current.MainWindow != window)
                {
                    window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    window.Owner = Application.Current.MainWindow;
                }
                else window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

                var haveDisplayName = model as IHaveDisplayName;
                if (haveDisplayName != null)
                {
                    var binding = new Binding("DisplayName") { Mode = BindingMode.TwoWay };
                    window.SetBinding(Window.TitleProperty, binding);
                }
            }
            else if (Application.Current != null
                   && Application.Current.MainWindow != null)
            {
                if (Application.Current.MainWindow != window && isDialog)
                    window.Owner = Application.Current.MainWindow;
            }

            return window;
        }

        void OnShutdownAttempted(IGuardClose guard, Window view, CancelEventArgs e)
        {
            if (actuallyClosing)
            {
                actuallyClosing = false;
                return;
            }

            bool runningAsync = false, shouldEnd = false;

            guard.CanClose(canClose => {
                if (runningAsync && canClose)
                {
                    actuallyClosing = true;
                    view.Close();
                }
                else e.Cancel = !canClose;

                shouldEnd = true;
            });

            if (shouldEnd)
                return;

            runningAsync = e.Cancel = true;
        }
    }
}