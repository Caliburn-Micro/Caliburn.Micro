#if XFORMS
namespace Caliburn.Micro.Xamarin.Forms
#else
namespace Caliburn.Micro
#endif 
{
    using System;
    using System.Linq;
#if WinRT
    using System.Reflection;
    using Windows.ApplicationModel;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Markup;
    using Windows.UI.Xaml.Media;
#elif XFORMS
    using System.Reflection;
    using global::Xamarin.Forms;
    using UIElement = global::Xamarin.Forms.Element;
    using FrameworkElement = global::Xamarin.Forms.VisualElement;
    using DependencyProperty = global::Xamarin.Forms.BindableProperty;
    using DependencyObject = global::Xamarin.Forms.BindableObject;
    using ContentControl = global::Xamarin.Forms.ContentView;
#else
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
#endif

    /// <summary>
    /// Hosts attached properties related to view models.
    /// </summary>
    public static class View {
        static readonly ILog Log = LogManager.GetLog(typeof(View));
#if WinRT || XFORMS
        const string DefaultContentPropertyName = "Content";
#else
        static readonly ContentPropertyAttribute DefaultContentProperty = new ContentPropertyAttribute("Content");
#endif

        /// <summary>
        /// A dependency property which allows the framework to track whether a certain element has already been loaded in certain scenarios.
        /// </summary>
        public static readonly DependencyProperty IsLoadedProperty =
            DependencyPropertyHelper.RegisterAttached(
                "IsLoaded",
                typeof(bool),
                typeof(View),
                false
                );

        /// <summary>
        /// A dependency property which marks an element as a name scope root.
        /// </summary>
        public static readonly DependencyProperty IsScopeRootProperty =
            DependencyPropertyHelper.RegisterAttached(
                "IsScopeRoot",
                typeof(bool),
                typeof(View),
                false
                );

        /// <summary>
        /// A dependency property which allows the override of convention application behavior.
        /// </summary>
        public static readonly DependencyProperty ApplyConventionsProperty =
            DependencyPropertyHelper.RegisterAttached(
                "ApplyConventions",
                typeof(bool?),
                typeof(View)
                );

        /// <summary>
        /// A dependency property for assigning a context to a particular portion of the UI.
        /// </summary>
        public static readonly DependencyProperty ContextProperty =
            DependencyPropertyHelper.RegisterAttached(
                "Context",
                typeof(object),
                typeof(View),
                null, 
                OnContextChanged
                );

        /// <summary>
        /// A dependency property for attaching a model to the UI.
        /// </summary>
        public static DependencyProperty ModelProperty =
            DependencyPropertyHelper.RegisterAttached(
                "Model",
                typeof(object),
                typeof(View),
                null, 
                OnModelChanged
                );

        /// <summary>
        /// Used by the framework to indicate that this element was generated.
        /// </summary>
        public static readonly DependencyProperty IsGeneratedProperty =
            DependencyPropertyHelper.RegisterAttached(
                "IsGenerated",
                typeof(bool),
                typeof(View),
                false
                );

        /// <summary>
        /// Executes the handler immediately if the element is loaded, otherwise wires it to the Loaded event.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="handler">The handler.</param>
        /// <returns>true if the handler was executed immediately; false otherwise</returns>
        public static bool ExecuteOnLoad(FrameworkElement element, RoutedEventHandler handler) {
#if XFORMS
            handler(element, new RoutedEventArgs());
            return true;
#else
#if SILVERLIGHT
            if ((bool)element.GetValue(IsLoadedProperty)) {
#elif WinRT
            if (IsElementLoaded(element)) {
#else
            if(element.IsLoaded) {
#endif
                handler(element, new RoutedEventArgs());
                return true;
            }

            RoutedEventHandler loaded = null;
            loaded = (s, e) => {
                element.Loaded -= loaded;
#if SILVERLIGHT
                element.SetValue(IsLoadedProperty, true);
#endif
                handler(s, e);
            };
            element.Loaded += loaded;
            return false;
#endif

        }

        /// <summary>
        /// Executes the handler when the element is unloaded.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="handler">The handler.</param>
        public static void ExecuteOnUnload(FrameworkElement element, RoutedEventHandler handler) {
#if !XFORMS
            RoutedEventHandler unloaded = null;
            unloaded = (s, e) => {
                element.Unloaded -= unloaded;
                handler(s, e);
            };
            element.Unloaded += unloaded;
#endif
        }

#if WinRT
        /// <summary>
        /// Determines whether the specified <paramref name="element"/> is loaded.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>true if the element is loaded; otherwise, false.
        /// </returns>
        public static bool IsElementLoaded(FrameworkElement element) {
            try
            {
                if ((element.Parent ?? VisualTreeHelper.GetParent(element)) != null)
                {
                    return true;
                }

                var rootVisual = Window.Current.Content;

                if (rootVisual != null)
                {
                    return element == rootVisual;
                }

                return false;

            }
            catch
            {
                return false;
            }
        }
#endif

        /// <summary>
        /// Executes the handler the next time the elements's LayoutUpdated event fires.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="handler">The handler.</param>
#if WinRT
        public static void ExecuteOnLayoutUpdated(FrameworkElement element, EventHandler<object> handler) {
            EventHandler<object> onLayoutUpdate = null;
#else
        public static void ExecuteOnLayoutUpdated(FrameworkElement element, EventHandler handler) {
            EventHandler onLayoutUpdate = null;
#endif
#if !XFORMS
            onLayoutUpdate = (s, e) => {
                element.LayoutUpdated -= onLayoutUpdate;
                handler(element, e);
            };
            element.LayoutUpdated += onLayoutUpdate;
#endif
        }

        /// <summary>
        /// Used to retrieve the root, non-framework-created view.
        /// </summary>
        /// <param name="view">The view to search.</param>
        /// <returns>The root element that was not created by the framework.</returns>
        /// <remarks>In certain instances the services create UI elements.
        /// For example, if you ask the window manager to show a UserControl as a dialog, it creates a window to host the UserControl in.
        /// The WindowManager marks that element as a framework-created element so that it can determine what it created vs. what was intended by the developer.
        /// Calling GetFirstNonGeneratedView allows the framework to discover what the original element was. 
        /// </remarks>
        public static Func<object, object> GetFirstNonGeneratedView = view => {
            var dependencyObject = view as DependencyObject;
            if (dependencyObject == null) {
                return view;
            }

            if ((bool)dependencyObject.GetValue(IsGeneratedProperty)) {
                if (dependencyObject is ContentControl) {
                    return ((ContentControl)dependencyObject).Content;
                }
#if WinRT || XFORMS
                var type = dependencyObject.GetType();
                var contentPropertyName = GetContentPropertyName(type);

                return type.GetRuntimeProperty(contentPropertyName)
                    .GetValue(dependencyObject, null);
#else
                var type = dependencyObject.GetType();
                var contentProperty = type.GetAttributes<ContentPropertyAttribute>(true)
                                          .FirstOrDefault() ?? DefaultContentProperty;

                return type.GetProperty(contentProperty.Name)
                    .GetValue(dependencyObject, null);
#endif
            }

            return dependencyObject;
        };

        /// <summary>
        /// Gets the convention application behavior.
        /// </summary>
        /// <param name="d">The element the property is attached to.</param>
        /// <returns>Whether or not to apply conventions.</returns>
        public static bool? GetApplyConventions(DependencyObject d) {
            return (bool?)d.GetValue(ApplyConventionsProperty);
        }

        /// <summary>
        /// Sets the convention application behavior.
        /// </summary>
        /// <param name="d">The element to attach the property to.</param>
        /// <param name="value">Whether or not to apply conventions.</param>
        public static void SetApplyConventions(DependencyObject d, bool? value) {
            d.SetValue(ApplyConventionsProperty, value);
        }

        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <param name="d">The element to attach the model to.</param>
        /// <param name="value">The model.</param>
        public static void SetModel(DependencyObject d, object value) {
            d.SetValue(ModelProperty, value);
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <param name="d">The element the model is attached to.</param>
        /// <returns>The model.</returns>
        public static object GetModel(DependencyObject d) {
            return d.GetValue(ModelProperty);
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <param name="d">The element the context is attached to.</param>
        /// <returns>The context.</returns>
        public static object GetContext(DependencyObject d) {
            return d.GetValue(ContextProperty);
        }

        /// <summary>
        /// Sets the context.
        /// </summary>
        /// <param name="d">The element to attach the context to.</param>
        /// <param name="value">The context.</param>
        public static void SetContext(DependencyObject d, object value) {
            d.SetValue(ContextProperty, value);
        }

        static void OnModelChanged(DependencyObject targetLocation, DependencyPropertyChangedEventArgs args) {
            if (args.OldValue == args.NewValue) {
                return;
            }

            if (args.NewValue != null) {
                var context = GetContext(targetLocation);
                
                var view = ViewLocator.LocateForModel(args.NewValue, targetLocation, context);
                // Trialing binding before setting content in Xamarin Forms
#if XFORMS
                ViewModelBinder.Bind(args.NewValue, view, context);
#endif
                if (!SetContentProperty(targetLocation, view)) {

                    Log.Warn("SetContentProperty failed for ViewLocator.LocateForModel, falling back to LocateForModelType");

                    view = ViewLocator.LocateForModelType(args.NewValue.GetType(), targetLocation, context);

                    SetContentProperty(targetLocation, view);
                }
#if !XFORMS
                ViewModelBinder.Bind(args.NewValue, view, context);
#endif
            }
            else {
                SetContentProperty(targetLocation, args.NewValue);
            }
        }

        static void OnContextChanged(DependencyObject targetLocation, DependencyPropertyChangedEventArgs e) {
            if (e.OldValue == e.NewValue) {
                return;
            }

            var model = GetModel(targetLocation);
            if (model == null) {
                return;
            }

            var view = ViewLocator.LocateForModel(model, targetLocation, e.NewValue);

            if (!SetContentProperty(targetLocation, view)) {

                Log.Warn("SetContentProperty failed for ViewLocator.LocateForModel, falling back to LocateForModelType");

                view = ViewLocator.LocateForModelType(model.GetType(), targetLocation, e.NewValue);

                SetContentProperty(targetLocation, view);
            }

            ViewModelBinder.Bind(model, view, e.NewValue);
        }

        static bool SetContentProperty(object targetLocation, object view) {
            var fe = view as FrameworkElement;
            if (fe != null && fe.Parent != null) {
                SetContentPropertyCore(fe.Parent, null);
            }

            return SetContentPropertyCore(targetLocation, view);
        }

#if WinRT || XFORMS
        static bool SetContentPropertyCore(object targetLocation, object view) {
            try {
                var type = targetLocation.GetType();
                var contentPropertyName = GetContentPropertyName(type);

                type.GetRuntimeProperty(contentPropertyName)
                    .SetValue(targetLocation, view, null);

                return true;
            }
            catch (Exception e) {
                Log.Error(e);

                return false;
            }
        }

        private static string GetContentPropertyName(Type type) {
            var typeInfo = type.GetTypeInfo();
            var contentProperty = typeInfo.GetCustomAttribute<ContentPropertyAttribute>();

            return contentProperty == null ? DefaultContentPropertyName : contentProperty.Name;
        }
#else
        static bool SetContentPropertyCore(object targetLocation, object view) {
            try {
                var type = targetLocation.GetType();
                var contentProperty = type.GetAttributes<ContentPropertyAttribute>(true)
                                          .FirstOrDefault() ?? DefaultContentProperty;

                type.GetProperty(contentProperty.Name)
                    .SetValue(targetLocation, view, null);

                return true;
            }
            catch(Exception e) {
                Log.Error(e);

                return false;
            }
        }
#endif

        private static bool? inDesignMode;

        /// <summary>
        /// Gets a value that indicates whether the process is running in design mode.
        /// </summary>
        public static bool InDesignMode
        {
            get
            {
                if (inDesignMode == null)
                {
#if XFORMS
                    inDesignMode = false;
#elif WinRT
                    inDesignMode = DesignMode.DesignModeEnabled;
#elif SILVERLIGHT
                    inDesignMode = DesignerProperties.IsInDesignTool;
#else
                    var descriptor = DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement));
                    inDesignMode = (bool)descriptor.Metadata.DefaultValue;
#endif
                }

                return inDesignMode.GetValueOrDefault(false);
            }
        }
    }
}
