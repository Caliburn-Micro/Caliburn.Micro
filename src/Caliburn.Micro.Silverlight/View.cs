namespace Caliburn.Micro
{
    using System;
    using System.Linq;
    using System.Reflection;
#if WinRT
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Markup;
#else
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
#endif

    /// <summary>
    /// Hosts attached properties related to view models.
    /// </summary>
    public static class View
    {
        static readonly ILog Log = LogManager.GetLog(typeof(View));
#if WinRT
        private const string DefaultContentPropertyName = "Content";
#else
        static readonly ContentPropertyAttribute DefaultContentProperty = new ContentPropertyAttribute("Content");
#endif

        /// <summary>
        /// The default view context.
        /// </summary>
        public static readonly object DefaultContext = new object();

        /// <summary>
        /// A dependency property which allows the framework to track whether a certain element has already been loaded in certain scenarios.
        /// </summary>
        public static readonly DependencyProperty IsLoadedProperty =
            DependencyProperty.RegisterAttached(
                "IsLoaded",
                typeof(bool),
                typeof(View),
                new PropertyMetadata(false)
                );

        /// <summary>
        /// A dependency property which marks an element as a name scope root.
        /// </summary>
        public static readonly DependencyProperty IsScopeRootProperty =
            DependencyProperty.RegisterAttached(
                "IsScopeRoot",
                typeof(bool),
                typeof(View),
                new PropertyMetadata(false)
                );

        /// <summary>
        /// A dependency property which allows the override of convention application behavior.
        /// </summary>
        public static readonly DependencyProperty ApplyConventionsProperty =
            DependencyProperty.RegisterAttached(
                "ApplyConventions",
                typeof(bool?),
                typeof(View),
                null
                );

        /// <summary>
        /// A dependency property for assigning a context to a particular portion of the UI.
        /// </summary>
        public static readonly DependencyProperty ContextProperty =
            DependencyProperty.RegisterAttached(
                "Context",
                typeof(object),
                typeof(View),
                new PropertyMetadata(null, OnContextChanged)
                );

        /// <summary>
        /// A dependency property for attaching a model to the UI.
        /// </summary>
        public static DependencyProperty ModelProperty =
            DependencyProperty.RegisterAttached(
                "Model",
                typeof(object),
                typeof(View),
                new PropertyMetadata(null, OnModelChanged)
                );

        /// <summary>
        /// Used by the framework to indicate that this element was generated.
        /// </summary>
        public static readonly DependencyProperty IsGeneratedProperty =
            DependencyProperty.RegisterAttached(
                "IsGenerated",
                typeof(bool),
                typeof(View),
                new PropertyMetadata(false)
                );

        /// <summary>
        /// Executes the handler immediately if the element is loaded, otherwise wires it to the Loaded event.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="handler">The handler.</param>
        /// <returns>true if the handler was executed immediately; false otherwise</returns>
        public static bool ExecuteOnLoad(FrameworkElement element, RoutedEventHandler handler)
        {
#if SILVERLIGHT || WinRT
            if ((bool)element.GetValue(IsLoadedProperty))
#else
            if(element.IsLoaded)
#endif
            {
                handler(element, new RoutedEventArgs());
                return true;
            }
            else
            {
                RoutedEventHandler loaded = null;
                loaded = (s, e) =>
                {
#if SILVERLIGHT
                    element.SetValue(IsLoadedProperty, true);
#endif
                    handler(s, e);
                    element.Loaded -= loaded;
                };

                element.Loaded += loaded;
                return false;
            }
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
        public static Func<object, object> GetFirstNonGeneratedView = view =>
        {
            var dependencyObject = view as DependencyObject;
            if (dependencyObject == null)
            {
                return view;
            }

            if ((bool)dependencyObject.GetValue(IsGeneratedProperty))
            {
                if (dependencyObject is ContentControl)
                {
                    return ((ContentControl)dependencyObject).Content;
                }
#if WinRT
                var type = dependencyObject.GetType();
                var typeInfo = type.GetTypeInfo();
                var contentProperty = typeInfo.GetAttributes<ContentPropertyAttribute>(true)
                    .FirstOrDefault();

                var contentPropertyName = contentProperty == null ? DefaultContentPropertyName : contentProperty.Name;

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
        public static bool? GetApplyConventions(DependencyObject d)
        {
            return (bool?)d.GetValue(ApplyConventionsProperty);
        }

        /// <summary>
        /// Sets the convention application behavior.
        /// </summary>
        /// <param name="d">The element to attach the property to.</param>
        /// <param name="value">Whether or not to apply conventions.</param>
        public static void SetApplyConventions(DependencyObject d, bool? value)
        {
            d.SetValue(ApplyConventionsProperty, value);
        }

        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <param name="d">The element to attach the model to.</param>
        /// <param name="value">The model.</param>
        public static void SetModel(DependencyObject d, object value)
        {
            d.SetValue(ModelProperty, value);
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <param name="d">The element the model is attached to.</param>
        /// <returns>The model.</returns>
        public static object GetModel(DependencyObject d)
        {
            return d.GetValue(ModelProperty);
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <param name="d">The element the context is attached to.</param>
        /// <returns>The context.</returns>
        public static object GetContext(DependencyObject d)
        {
            return d.GetValue(ContextProperty);
        }

        /// <summary>
        /// Sets the context.
        /// </summary>
        /// <param name="d">The element to attach the context to.</param>
        /// <param name="value">The context.</param>
        public static void SetContext(DependencyObject d, object value)
        {
            d.SetValue(ContextProperty, value);
        }

        static void OnModelChanged(DependencyObject targetLocation, DependencyPropertyChangedEventArgs args)
        {
            if (args.OldValue == args.NewValue)
            {
                return;
            }

            if (args.NewValue != null)
            {
                var context = GetContext(targetLocation);
                var view = ViewLocator.LocateForModel(args.NewValue, targetLocation, context);

                SetContentProperty(targetLocation, view);
                ViewModelBinder.Bind(args.NewValue, view, context);
            }
            else
            {
                SetContentProperty(targetLocation, args.NewValue);
            }
        }

        static void OnContextChanged(DependencyObject targetLocation, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue == e.NewValue)
            {
                return;
            }

            var model = GetModel(targetLocation);
            if (model == null)
            {
                return;
            }

            var view = ViewLocator.LocateForModel(model, targetLocation, e.NewValue);

            SetContentProperty(targetLocation, view);
            ViewModelBinder.Bind(model, view, e.NewValue);
        }

        static void SetContentProperty(object targetLocation, object view)
        {
            var fe = view as FrameworkElement;
            if (fe != null && fe.Parent != null)
            {
                SetContentPropertyCore(fe.Parent, null);
            }

            SetContentPropertyCore(targetLocation, view);
        }

#if WinRT
        static void SetContentPropertyCore(object targetLocation, object view)
        {
            try
            {
                var type = targetLocation.GetType();
                var typeInfo = type.GetTypeInfo();
                var contentProperty = typeInfo.GetAttributes<ContentPropertyAttribute>(true)
                                          .FirstOrDefault();

                var contentPropertyName = contentProperty == null ? DefaultContentPropertyName : contentProperty.Name;

                type.GetRuntimeProperty(contentPropertyName)
                    .SetValue(targetLocation, view, null);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
#else
        static void SetContentPropertyCore(object targetLocation, object view) {
            try {
                var type = targetLocation.GetType();
                var contentProperty = type.GetAttributes<ContentPropertyAttribute>(true)
                                          .FirstOrDefault() ?? DefaultContentProperty;

                type.GetProperty(contentProperty.Name)
                    .SetValue(targetLocation, view, null);
            }
            catch(Exception e) {
                Log.Error(e);
            }
        }
#endif
    }
}