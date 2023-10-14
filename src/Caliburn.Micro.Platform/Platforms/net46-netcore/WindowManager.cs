using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Navigation;

namespace Caliburn.Micro {
    /// <summary>
    /// A service that manages windows.
    /// </summary>
    public class WindowManager : IWindowManager {
        /// <summary>
        /// Shows a modal dialog for the specified model.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="context">The context.</param>
        /// <param name="settings">The dialog popup settings.</param>
        /// <returns>The dialog result.</returns>
        public virtual async Task<bool?> ShowDialogAsync(object rootModel, object context = null, IDictionary<string, object> settings = null) {
            Window window = await CreateWindowAsync(rootModel, true, context, settings);

            return window.ShowDialog();
        }

        /// <summary>
        /// Shows a window for the specified model.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="context">The context.</param>
        /// <param name="settings">The optional window settings.</param>
        public virtual async Task ShowWindowAsync(object rootModel, object context = null, IDictionary<string, object> settings = null) {
            NavigationWindow navWindow = null;

            Application application = Application.Current;
            if (application != null && application.MainWindow != null) {
                navWindow = application.MainWindow as NavigationWindow;
            }

            if (navWindow != null) {
                Page window = await CreatePageAsync(rootModel, context, settings);
                navWindow.Navigate(window);
            } else {
                Window window = await CreateWindowAsync(rootModel, false, context, settings);

                window.Show();
            }
        }

        /// <summary>
        /// Shows a popup at the current mouse position.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="context">The view context.</param>
        /// <param name="settings">The optional popup settings.</param>
        public virtual async Task ShowPopupAsync(object rootModel, object context = null, IDictionary<string, object> settings = null) {
            Popup popup = CreatePopup(rootModel, settings);
            UIElement view = ViewLocator.LocateForModel(rootModel, popup, context);

            popup.Child = view;
            popup.SetValue(View.IsGeneratedProperty, true);

            ViewModelBinder.Bind(rootModel, popup, null);
            Action.SetTargetWithoutContext(view, rootModel);

            if (rootModel is IActivate activator) {
                await activator.ActivateAsync();
            }

            if (rootModel is IDeactivate deactivator) {
                popup.Closed += async (s, e) => await deactivator.DeactivateAsync(true);
            }

            popup.IsOpen = true;
            popup.CaptureMouse();
        }

        /// <summary>
        /// Creates the page.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="context">The context.</param>
        /// <param name="settings">The optional popup settings.</param>
        /// <returns>The page.</returns>
        public virtual async Task<Page> CreatePageAsync(object rootModel, object context, IDictionary<string, object> settings) {
            Page view = EnsurePage(rootModel, ViewLocator.LocateForModel(rootModel, null, context));
            ViewModelBinder.Bind(rootModel, view, context);

            if (string.IsNullOrEmpty(view.Title) && rootModel is IHaveDisplayName haveDisplayName && !ConventionManager.HasBinding(view, Page.TitleProperty)) {
                var binding = new Binding("DisplayName") { Mode = BindingMode.TwoWay };
                view.SetBinding(Page.TitleProperty, binding);
            }

            ApplySettings(view, settings);

            if (rootModel is IActivate activator) {
                await activator.ActivateAsync();
            }

            if (rootModel is IDeactivate deactivatable) {
                view.Unloaded += async (s, e) => await deactivatable.DeactivateAsync(true);
            }

            return view;
        }

        /// <summary>
        /// Creates a popup for hosting a popup window.
        /// </summary>
        /// <param name="rootModel">The model.</param>
        /// <param name="settings">The optional popup settings.</param>
        /// <returns>The popup.</returns>
        protected virtual Popup CreatePopup(object rootModel, IDictionary<string, object> settings) {
            var popup = new Popup();

            if (ApplySettings(popup, settings)) {
                if (!settings.ContainsKey("PlacementTarget") && !settings.ContainsKey("Placement")) {
                    popup.Placement = PlacementMode.MousePoint;
                }

                if (!settings.ContainsKey("AllowsTransparency")) {
                    popup.AllowsTransparency = true;
                }
            } else {
                popup.AllowsTransparency = true;
                popup.Placement = PlacementMode.MousePoint;
            }

            return popup;
        }

        /// <summary>
        /// Creates a window.
        /// </summary>
        /// <param name="rootModel">The view model.</param>
        /// <param name="isDialog">Whethor or not the window is being shown as a dialog.</param>
        /// <param name="context">The view context.</param>
        /// <param name="settings">The optional popup settings.</param>
        /// <returns>The window.</returns>
        protected virtual async Task<Window> CreateWindowAsync(object rootModel, bool isDialog, object context, IDictionary<string, object> settings) {
            Window view = EnsureWindow(rootModel, ViewLocator.LocateForModel(rootModel, null, context), isDialog);
            ViewModelBinder.Bind(rootModel, view, context);
            if (string.IsNullOrEmpty(view.Title) && rootModel is IHaveDisplayName && !ConventionManager.HasBinding(view, Window.TitleProperty)) {
                var binding = new Binding("DisplayName") { Mode = BindingMode.TwoWay };
                view.SetBinding(Window.TitleProperty, binding);
            }

            ApplySettings(view, settings);

            var conductor = new WindowConductor(rootModel, view);

            await conductor.InitialiseAsync();

            return view;
        }

        /// <summary>
        /// Makes sure the view is a window is is wrapped by one.
        /// </summary>
        /// <param name="model">The view model.</param>
        /// <param name="view">The view.</param>
        /// <param name="isDialog">Whethor or not the window is being shown as a dialog.</param>
        /// <returns>The window.</returns>
        protected virtual Window EnsureWindow(object model, object view, bool isDialog) {
            if (view is Window window) {
                Window owner = InferOwnerOf(window);
                if (owner != null && isDialog) {
                    window.Owner = owner;
                }
            } else {
                window = new Window {
                    Content = view,
                    SizeToContent = SizeToContent.WidthAndHeight,
                };

                window.SetValue(View.IsGeneratedProperty, true);

                Window owner = InferOwnerOf(window);
                if (owner != null) {
                    window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    window.Owner = owner;
                } else {
                    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }
            }

            return window;
        }

        /// <summary>
        /// Infers the owner of the window.
        /// </summary>
        /// <param name="window">The window to whose owner needs to be determined.</param>
        /// <returns>The owner.</returns>
        protected virtual Window InferOwnerOf(Window window) {
            Application application = Application.Current;
            if (application == null) {
                return null;
            }

            Window active = application.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
            active ??= PresentationSource.FromVisual(application.MainWindow) == null ? null : application.MainWindow;
            return active == window ? null : active;
        }

        /// <summary>
        /// Ensures the view is a page or provides one.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="view">The view.</param>
        /// <returns>The page.</returns>
        protected virtual Page EnsurePage(object model, object view) {
            if (view is Page page) {
                return page;
            }

            page = new Page { Content = view };
            page.SetValue(View.IsGeneratedProperty, true);

            return page;
        }

        private bool ApplySettings(object target, IEnumerable<KeyValuePair<string, object>> settings) {
            if (settings != null) {
                Type type = target.GetType();

                foreach (KeyValuePair<string, object> pair in settings) {
                    System.Reflection.PropertyInfo propertyInfo = type.GetProperty(pair.Key);

                    propertyInfo?.SetValue(target, pair.Value, null);
                }

                return true;
            }

            return false;
        }
    }
}
