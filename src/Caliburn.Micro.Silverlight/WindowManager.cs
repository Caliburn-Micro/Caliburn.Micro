namespace Caliburn.Micro
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public interface IWindowManager
    {
        void ShowDialog(object rootModel, object context = null);
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
            if((bool)view.GetValue(IsElementGeneratedProperty))
                return (DependencyObject)((ContentControl)view).Content;

            return view;
        }

        public void ShowDialog(object rootModel, object context = null)
        {
            var view = EnsureWindow(rootModel, ViewLocator.LocateForModel(rootModel, context));
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

            view.Show();
        }

        static ChildWindow EnsureWindow(object model, object view)
        {
            var window = view as ChildWindow;

            if(window == null)
            {
                window = new ChildWindow { Content = view };
                window.SetValue(IsElementGeneratedProperty, true);

                var haveDisplayName = model as IHaveDisplayName;
                if(haveDisplayName != null)
                {
                    var binding = new Binding("DisplayName") { Mode = BindingMode.TwoWay };
                    window.SetBinding(ChildWindow.TitleProperty, binding);
                }
            }

            return window;
        }

        void OnShutdownAttempted(IGuardClose guard, ChildWindow view, CancelEventArgs e)
        {
            if (actuallyClosing)
            {
                actuallyClosing = false;
                return;
            }

            bool runningAsync = false, shouldEnd = false;

            guard.CanClose(canClose =>{
                if(runningAsync && canClose)
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