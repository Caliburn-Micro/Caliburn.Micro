using System;

#if WINDOWS_UWP
using System.Reflection;

using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

#elif XFORMS
using System.Reflection;

using Xamarin.Forms;

using ContentControl = Xamarin.Forms.ContentView;
using DependencyObject = Xamarin.Forms.BindableObject;
using DependencyProperty = Xamarin.Forms.BindableProperty;
using FrameworkElement = Xamarin.Forms.VisualElement;
using UIElement = Xamarin.Forms.Element;

#elif MAUI
using System.Reflection;

/* using Microsoft.UI.Xaml; */
using Microsoft.Maui.Controls;

using ContentControl = Microsoft.Maui.Controls.ContentView;
using DependencyObject = Microsoft.Maui.Controls.BindableObject;
using DependencyProperty = Microsoft.Maui.Controls.BindableProperty;
using FrameworkElement = Microsoft.Maui.Controls.VisualElement;
using UIElement = Microsoft.Maui.Controls.Element;

#else
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
#endif

#if XFORMS
namespace Caliburn.Micro.Xamarin.Forms
#elif MAUI
namespace Caliburn.Micro.Maui
#else
namespace Caliburn.Micro
#endif
{
    /// <summary>
    /// Hosts attached properties related to view models.
    /// </summary>
    public static class View {
        /// <summary>
        /// A dependency property which allows the framework to track whether a certain element has already been loaded in certain scenarios.
        /// </summary>
        public static readonly DependencyProperty IsLoadedProperty =
            DependencyPropertyHelper.RegisterAttached(
                "IsLoaded",
                typeof(bool),
                typeof(View),
                false);

        /// <summary>
        /// A dependency property which marks an element as a name scope root.
        /// </summary>
        public static readonly DependencyProperty IsScopeRootProperty =
            DependencyPropertyHelper.RegisterAttached(
                "IsScopeRoot",
                typeof(bool),
                typeof(View),
                false);

        /// <summary>
        /// A dependency property which allows the override of convention application behavior.
        /// </summary>
        public static readonly DependencyProperty ApplyConventionsProperty =
            DependencyPropertyHelper.RegisterAttached(
                "ApplyConventions",
                typeof(bool?),
                typeof(View));

        /// <summary>
        /// A dependency property for assigning a context to a particular portion of the UI.
        /// </summary>
        public static readonly DependencyProperty ContextProperty =
            DependencyPropertyHelper.RegisterAttached(
                "Context",
                typeof(object),
                typeof(View),
                null,
                OnContextChanged);

        /// <summary>
        /// Used by the framework to indicate that this element was generated.
        /// </summary>
        public static readonly DependencyProperty IsGeneratedProperty =
            DependencyPropertyHelper.RegisterAttached(
                "IsGenerated",
                typeof(bool),
                typeof(View),
                false);

        /// <summary>
        /// A dependency property for attaching a model to the UI.
        /// </summary>
        public static readonly DependencyProperty ModelProperty =
            DependencyPropertyHelper.RegisterAttached(
                "Model",
                typeof(object),
                typeof(View),
                null,
                OnModelChanged);

#if WINDOWS_UWP || XFORMS || MAUI
        private const string DefaultContentPropertyName = "Content";
#else
        private static readonly ContentPropertyAttribute DefaultContentProperty
            = new ContentPropertyAttribute("Content");
#endif

        private static readonly ILog Log = LogManager.GetLog(typeof(View));

        private static bool? inDesignMode;

        /// <summary>
        /// Gets a value indicating whether the process is running in design mode.
        /// </summary>
        public static bool InDesignMode {
            get {
                if (inDesignMode == null) {
#if XFORMS || MAUI
                    inDesignMode = false;
#elif WINDOWS_UWP
                    inDesignMode = DesignMode.DesignModeEnabled;
#else
                    var descriptor = DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement));
                    inDesignMode = (bool)descriptor.Metadata.DefaultValue;
#endif
                }

                return inDesignMode.GetValueOrDefault(false);
            }
        }

        /// <summary>
        /// Gets or sets func used to retrieve the root, non-framework-created view.
        /// </summary>
        /// <remarks>In certain instances the services create UI elements.
        /// For example, if you ask the window manager to show a UserControl as a dialog, it creates a window to host the UserControl in.
        /// The WindowManager marks that element as a framework-created element so that it can determine what it created vs. what was intended by the developer.
        /// Calling GetFirstNonGeneratedView allows the framework to discover what the original element was.
        /// </remarks>
        public static Func<object, object> GetFirstNonGeneratedView { get; set; }
            = view => {
                if (!(view is DependencyObject dependencyObject)) {
                    return view;
                }

                if ((bool)dependencyObject.GetValue(IsGeneratedProperty)) {
                    if (dependencyObject is ContentControl control) {
                        return control.Content;
                    }

#if WINDOWS_UWP || XFORMS || MAUI
                    Type type = dependencyObject.GetType();
                    string contentPropertyName = GetContentPropertyName(type);

                    return type.GetRuntimeProperty(contentPropertyName)
                        .GetValue(dependencyObject, null);
#else
                    Type type = dependencyObject.GetType();
                    ContentPropertyAttribute contentProperty = type.GetCustomAttributes(typeof(ContentPropertyAttribute), true)
                                              .OfType<ContentPropertyAttribute>()
                                              .FirstOrDefault() ?? DefaultContentProperty;

                    return type.GetProperty(contentProperty.Name)
                        .GetValue(dependencyObject, null);
#endif
                }

                return dependencyObject;
            };

        /// <summary>
        /// Executes the handler immediately if the element is loaded, otherwise wires it to the Loaded event.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="handler">The handler.</param>
        /// <returns>true if the handler was executed immediately; false otherwise.</returns>
        public static bool ExecuteOnLoad(FrameworkElement element, RoutedEventHandler handler) {
#if XFORMS || MAUI
            handler(element, new RoutedEventArgs());
            return true;
#else
#if WINDOWS_UWP
            if (IsElementLoaded(element)) {
#else
            if (element.IsLoaded) {
#endif
                handler(element, new RoutedEventArgs());
                return true;
            }

            void OnLoaded(object s, RoutedEventArgs e) {
                element.Loaded -= OnLoaded;
                handler(s, e);
            }

            element.Loaded += OnLoaded;

            return false;
#endif
        }

        /// <summary>
        /// Executes the handler when the element is unloaded.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="handler">The handler.</param>
        public static void ExecuteOnUnload(FrameworkElement element, RoutedEventHandler handler) {
#if !XFORMS && !MAUI
            void OnUnloaded(object s, RoutedEventArgs e) {
                element.Unloaded -= OnUnloaded;
                handler(s, e);
            }

            element.Unloaded += OnUnloaded;
#endif
        }

#if WINDOWS_UWP
        /// <summary>
        /// Determines whether the specified <paramref name="element"/> is loaded.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>true if the element is loaded; otherwise, false.
        /// </returns>
        public static bool IsElementLoaded(FrameworkElement element) {
            try {
                if ((element.Parent ?? VisualTreeHelper.GetParent(element)) != null) {
                    return true;
                }

                UIElement rootVisual = Window.Current.Content;

                return rootVisual != null &&
                       rootVisual == element;
            } catch {
                return false;
            }
        }
#endif

#if !XFORMS && !MAUI
        /// <summary>
        /// Executes the handler the next time the elements's LayoutUpdated event fires.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="handler">The handler.</param>
#if WINDOWS_UWP //|| MAUI
        public static void ExecuteOnLayoutUpdated(FrameworkElement element, EventHandler<object> handler) {
            EventHandler<object> onLayoutUpdate = null;
#elif false
        public static void ExecuteOnLayoutUpdated(FrameworkElement element, EventHandler<object> handler) {
            EventHandler<object> onLayoutUpdate = null;
#else
        public static void ExecuteOnLayoutUpdated(FrameworkElement element, EventHandler handler) {
            EventHandler onLayoutUpdate = null;
#endif
            onLayoutUpdate = (s, e) => {
                element.LayoutUpdated -= onLayoutUpdate;
                handler(element, e);
            };
            element.LayoutUpdated += onLayoutUpdate;
        }
#endif

        /// <summary>
        /// Gets the convention application behavior.
        /// </summary>
        /// <param name="d">The element the property is attached to.</param>
        /// <returns>Whether or not to apply conventions.</returns>
        public static bool? GetApplyConventions(DependencyObject d)
            => (bool?)d.GetValue(ApplyConventionsProperty);

        /// <summary>
        /// Sets the convention application behavior.
        /// </summary>
        /// <param name="d">The element to attach the property to.</param>
        /// <param name="value">Whether or not to apply conventions.</param>
        public static void SetApplyConventions(DependencyObject d, bool? value)
            => d.SetValue(ApplyConventionsProperty, value);

        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <param name="d">The element to attach the model to.</param>
        /// <param name="value">The model.</param>
        public static void SetModel(DependencyObject d, object value) => d.SetValue(ModelProperty, value);

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <param name="d">The element the model is attached to.</param>
        /// <returns>The model.</returns>
        public static object GetModel(DependencyObject d) => d.GetValue(ModelProperty);

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <param name="d">The element the context is attached to.</param>
        /// <returns>The context.</returns>
        public static object GetContext(DependencyObject d) => d.GetValue(ContextProperty);

        /// <summary>
        /// Sets the context.
        /// </summary>
        /// <param name="d">The element to attach the context to.</param>
        /// <param name="value">The context.</param>
        public static void SetContext(DependencyObject d, object value) => d.SetValue(ContextProperty, value);

        private static void OnModelChanged(DependencyObject targetLocation, DependencyPropertyChangedEventArgs args) {
            if (args.OldValue == args.NewValue) {
                return;
            }

            if (args.NewValue != null) {
                object context = GetContext(targetLocation);

                UIElement view = ViewLocator.LocateForModel(args.NewValue, targetLocation, context);
                ViewModelBinder.Bind(args.NewValue, view, context);
                if (!SetContentProperty(targetLocation, view)) {
                    Log.Warn("SetContentProperty failed for ViewLocator.LocateForModel, falling back to LocateForModelType");

                    view = ViewLocator.LocateForModelType(args.NewValue.GetType(), targetLocation, context);

                    SetContentProperty(targetLocation, view);
                }
            } else {
                SetContentProperty(targetLocation, args.NewValue);
            }
        }

        private static void OnContextChanged(DependencyObject targetLocation, DependencyPropertyChangedEventArgs e) {
            if (e.OldValue == e.NewValue) {
                return;
            }

            object model = GetModel(targetLocation);
            if (model == null) {
                return;
            }

            UIElement view = ViewLocator.LocateForModel(model, targetLocation, e.NewValue);

            if (!SetContentProperty(targetLocation, view)) {
                Log.Warn("SetContentProperty failed for ViewLocator.LocateForModel, falling back to LocateForModelType");

                view = ViewLocator.LocateForModelType(model.GetType(), targetLocation, e.NewValue);

                SetContentProperty(targetLocation, view);
            }

            ViewModelBinder.Bind(model, view, e.NewValue);
        }

        private static bool SetContentProperty(object targetLocation, object view) {
            if (view is FrameworkElement fe && fe.Parent != null) {
                SetContentPropertyCore(fe.Parent, null);
            }

            return SetContentPropertyCore(targetLocation, view);
        }

#if WINDOWS_UWP || XFORMS || MAUI
        private static bool SetContentPropertyCore(object targetLocation, object view) {
            try {
                Type type = targetLocation.GetType();
                string contentPropertyName = GetContentPropertyName(type);

                type.GetRuntimeProperty(contentPropertyName)
                    .SetValue(targetLocation, view, null);

                return true;
            } catch (Exception e) {
                Log.Error(e);

                return false;
            }
        }

        private static string GetContentPropertyName(Type type) {
            TypeInfo typeInfo = type.GetTypeInfo();
            ContentPropertyAttribute contentProperty = typeInfo.GetCustomAttribute<ContentPropertyAttribute>();

            return contentProperty?.Name ?? DefaultContentPropertyName;
        }
#else
        private static bool SetContentPropertyCore(object targetLocation, object view) {
            try {
                Type type = targetLocation.GetType();
                ContentPropertyAttribute contentProperty = type.GetCustomAttributes(typeof(ContentPropertyAttribute), true)
                                          .OfType<ContentPropertyAttribute>()
                                          .FirstOrDefault() ?? DefaultContentProperty;

                type.GetProperty(contentProperty?.Name ?? DefaultContentProperty.Name)
                    .SetValue(targetLocation, view, null);

                return true;
            } catch (Exception e) {
                Log.Error(e);

                return false;
            }
        }
#endif
    }
}
