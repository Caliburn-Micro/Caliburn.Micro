using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Data;

namespace Caliburn.Micro
{
    /// <summary>
    /// A service that manages windows.
    /// </summary>
    public class WindowManager : IWindowManager
    {
        static readonly ILog Log = LogManager.GetLog(typeof(WindowManager));
        /// <summary>
        /// Shows a modal dialog for the specified model.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="context">The context.</param>
        /// <param name="settings">The dialog popup settings.</param>
        /// <returns>The dialog result.</returns>
        public virtual async Task<bool?> ShowDialogAsync(object rootModel, object context = null, IDictionary<string, object> settings = null)
        {
            var window = await CreateWindowAsync(rootModel, context, settings);

            return await window.ShowDialog<bool?>(InferOwnerOf(window));
        }

        /// <summary>
        /// Shows a window for the specified model.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="context">The context.</param>
        /// <param name="settings">The optional window settings.</param>
        public virtual async Task ShowWindowAsync(object rootModel, object context = null, IDictionary<string, object> settings = null)
        {
            /*
            NavigationWindow navWindow = null;

            var application = Application.Current;
            if (application != null && application.MainWindow != null)
            {
                navWindow = application.MainWindow as NavigationWindow;
            }

            if (navWindow != null)
            {
                var window = await CreatePageAsync(rootModel, context, settings);
                navWindow.Navigate(window);
            }
            else
            {
            */
            var window = await CreateWindowAsync(rootModel, context, settings);

            window.Show();
            /*}*/
        }

        /// <summary>
        /// Shows a popup at the current mouse position.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="context">The view context.</param>
        /// <param name="settings">The optional popup settings.</param>
        public virtual async Task ShowPopupAsync(object rootModel, object context = null, IDictionary<string, object> settings = null)
        {
            var popup = CreatePopup(rootModel, settings);
            var view = ViewLocator.LocateForModel(rootModel, popup, context);

            popup.Child = view;
            popup.SetValue(View.IsGeneratedProperty, true);

            ViewModelBinder.Bind(rootModel, popup, null);
            Action.SetTargetWithoutContext(view, rootModel);

            if (rootModel is IActivate activator)
            {
                await activator.ActivateAsync();
            }

            if (rootModel is IDeactivate deactivator)
            {
                popup.Closed += async (s, e) => await deactivator.DeactivateAsync(true);
            }

            popup.IsOpen = true;
            //popup.CaptureMouse(); //todo: mouse capture
        }

        /// <summary>
        /// Creates a popup for hosting a popup window.
        /// </summary>
        /// <param name="rootModel">The model.</param>
        /// <param name="settings">The optional popup settings.</param>
        /// <returns>The popup.</returns>
        protected virtual Popup CreatePopup(object rootModel, IDictionary<string, object> settings)
        {
            var popup = new Popup();

            if (ApplySettings(popup, settings))
            {
                if (!settings.ContainsKey("PlacementTarget") && !settings.ContainsKey("Placement"))
                {
                    popup.Placement = PlacementMode.Pointer;
                }

                //if (!settings.ContainsKey("AllowsTransparency"))
                //{
                //    popup.AllowsTransparency = true;
                //}
            }
            else
            {
                //popup.AllowsTransparency = true;
                popup.Placement = PlacementMode.Pointer;
            }

            return popup;
        }

        /// <summary>
        /// Creates a window.
        /// </summary>
        /// <param name="rootModel">The view model.</param>
        /// <param name="context">The view context.</param>
        /// <param name="settings">The optional popup settings.</param>
        /// <returns>The window.</returns>
        public virtual async Task<Window> CreateWindowAsync(object rootModel, object context, IDictionary<string, object> settings)
        {
            Log.Info("Creating window for {0}.", rootModel);
            Control view1 = ViewLocator.LocateForModel(rootModel, null, context);
            Log.Info("Calling EnsureWindow");
            var view = EnsureWindow(rootModel, view1);

            var haveDisplayName = rootModel as IHaveDisplayName;
            if ((string.IsNullOrEmpty(view.Title) || view.Title == "Window") && haveDisplayName != null && !ConventionManager.HasBinding(view, Window.TitleProperty))
            {
                var binding = new Binding("DisplayName") { Mode = BindingMode.TwoWay };
                view.Bind(Window.TitleProperty, binding);
            }            

            ApplySettings(view, settings);
            var conductor = new WindowConductor(rootModel, view);

            await conductor.InitialiseAsync();
            ViewModelBinder.Bind(rootModel, view, context);
            return view;
        }

        /// <summary>
        /// Makes sure the view is a window or is wrapped by one.
        /// </summary>
        /// <param name="model">The view model.</param>
        /// <param name="view">The view.</param>
        /// <returns>The window.</returns>
        protected virtual Window EnsureWindow(object model, object view)
        {
            if (!(view is Window window))
            {
                window = new Window
                {
                    Content = view,
                    SizeToContent = SizeToContent.WidthAndHeight
                };
                window.SetValue(View.IsGeneratedProperty, true);
                window.WindowStartupLocation = InferOwnerOf(window) != null
                    ? WindowStartupLocation.CenterOwner
                    : WindowStartupLocation.CenterScreen;
            }
            return window;
        }

        /// <summary>
        /// Infers the owner of the window.
        /// </summary>
        /// <param name="window">The window to whose owner needs to be determined.</param>
        /// <returns>The owner.</returns>
        protected virtual Window InferOwnerOf(Window window)
        {
            var application = Application.Current;
            if (application == null)
            {
                return null;
            }

            Window active = null;
            if (application.ApplicationLifetime is ClassicDesktopStyleApplicationLifetime c)
            {
                active = c.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
                active = active ?? c.MainWindow; //(PresentationSource.FromVisual(application.MainWindow) == null ? null : application.MainWindow);
            }

            return active == window ? null : active;
        }



        private bool ApplySettings(object target, IEnumerable<KeyValuePair<string, object>> settings)
        {
            if (settings != null)
            {
                var type = target.GetType();

                foreach (var pair in settings)
                {
                    var propertyInfo = type.GetProperty(pair.Key);

                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(target, pair.Value, null);
                    }
                }

                return true;
            }

            return false;
        }
    }
}
