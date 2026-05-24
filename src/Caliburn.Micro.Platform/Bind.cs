#if XFORMS
namespace Caliburn.Micro.Xamarin.Forms
#elif MAUI
namespace Caliburn.Micro.Maui
#else
namespace Caliburn.Micro
#endif
{
    using System;
#if WINDOWS_UWP
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;
#elif WinUI3
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Data;
#elif XFORMS
    using global::Xamarin.Forms;
    using UIElement = global::Xamarin.Forms.Element;
    using FrameworkElement = global::Xamarin.Forms.VisualElement;
    using DependencyProperty = global::Xamarin.Forms.BindableProperty;
    using DependencyObject = global::Xamarin.Forms.BindableObject;
#elif AVALONIA
    using Avalonia;
    using Avalonia.Data;
    using DependencyObject = Avalonia.AvaloniaObject;
    using DependencyPropertyChangedEventArgs = Avalonia.AvaloniaPropertyChangedEventArgs;
    using FrameworkElement = Avalonia.Controls.Control;
    using DependencyProperty = Avalonia.AvaloniaProperty;
#elif MAUI
    using global::Microsoft.Maui;
    using UIElement = global::Microsoft.Maui.Controls.Element;
    using FrameworkElement = global::Microsoft.Maui.Controls.VisualElement;
    using DependencyProperty = global::Microsoft.Maui.Controls.BindableProperty;
    using DependencyObject = global::Microsoft.Maui.Controls.BindableObject;
#else
    using System.Windows;
    using System.Windows.Data;
#endif

    /// <summary>
    ///   Hosts dependency properties for binding.
    /// </summary>
    public static class Bind
    {
        /// <summary>
        ///   Allows binding on an existing view. Use this on root UserControls, Pages and Windows; not in a DataTemplate.
        /// </summary>
        public static DependencyProperty ModelProperty =
#if AVALONIA
            AvaloniaProperty.RegisterAttached<AvaloniaObject, object>("Model", typeof(Bind));
#else
            DependencyPropertyHelper.RegisterAttached(
                "Model",
                typeof(object),
                typeof(Bind),
                null,
                ModelChanged);
#endif

        /// <summary>
        ///   Allows binding on an existing view without setting the data context. Use this from within a DataTemplate.
        /// </summary>
        public static DependencyProperty ModelWithoutContextProperty =
#if AVALONIA
            AvaloniaProperty.RegisterAttached<AvaloniaObject, object>("Handler", typeof(Bind));
#else
            DependencyPropertyHelper.RegisterAttached(
                "ModelWithoutContext",
                typeof(object),
                typeof(Bind),
                null,
                ModelWithoutContextChanged);
#endif

        internal static readonly DependencyProperty NoContextProperty =
#if AVALONIA
            AvaloniaProperty.RegisterAttached<AvaloniaObject, bool>("Handler", typeof(Bind));
#else
            DependencyPropertyHelper.RegisterAttached(
                "NoContext",
                typeof(bool),
                typeof(Bind),
                false);
#endif

#if AVALONIA
        static Bind()
        {
            ModelProperty.Changed.Subscribe(args => ModelChanged(args.Sender, args));
            ModelWithoutContextProperty.Changed.Subscribe(args => ModelWithoutContextChanged(args.Sender, args));
            DataContextProperty.Changed.Subscribe(args => DataContextChanged(args.Sender, args));
            AtDesignTimeProperty.Changed.Subscribe(args => AtDesignTimeChanged(args.Sender, args));
        }
#endif
        /// <summary>
        ///   Gets the model to bind to.
        /// </summary>
        /// <param name = "dependencyObject">The dependency object to bind to.</param>
        /// <returns>The model.</returns>
        public static object GetModelWithoutContext(DependencyObject dependencyObject)
        {
            return dependencyObject.GetValue(ModelWithoutContextProperty);
        }

        /// <summary>
        ///   Sets the model to bind to.
        /// </summary>
        /// <param name = "dependencyObject">The dependency object to bind to.</param>
        /// <param name = "value">The model.</param>
        public static void SetModelWithoutContext(DependencyObject dependencyObject, object value)
        {
            dependencyObject.SetValue(ModelWithoutContextProperty, value);
        }

        /// <summary>
        ///   Gets the model to bind to.
        /// </summary>
        /// <param name = "dependencyObject">The dependency object to bind to.</param>
        /// <returns>The model.</returns>
        public static object GetModel(DependencyObject dependencyObject)
        {
            return dependencyObject.GetValue(ModelProperty);
        }

        /// <summary>
        ///   Sets the model to bind to.
        /// </summary>
        /// <param name = "dependencyObject">The dependency object to bind to.</param>
        /// <param name = "value">The model.</param>
        public static void SetModel(DependencyObject dependencyObject, object value)
        {
            dependencyObject.SetValue(ModelProperty, value);
        }

        static void ModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (View.InDesignMode || e.NewValue == null || object.ReferenceEquals(e.NewValue, e.OldValue))
            {
                return;
            }

            var fe = d as FrameworkElement;
            if (fe == null)
            {
                return;
            }

            View.ExecuteOnLoad(fe, delegate
            {
                var target = e.NewValue;

                d.SetValue(View.IsScopeRootProperty, true);

#if XFORMS || MAUI
                var context = fe.Id.ToString("N");
#else
                var context = string.IsNullOrEmpty(fe.Name)
                                  ? fe.GetHashCode().ToString()
                                  : fe.Name;
#endif

                ViewModelBinder.Bind(target, d, context);
            });
        }

        static void ModelWithoutContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (View.InDesignMode || e.NewValue == null || object.ReferenceEquals(e.NewValue, e.OldValue))
            {
                return;
            }

            var fe = d as FrameworkElement;
            if (fe == null)
            {
                return;
            }

            View.ExecuteOnLoad(fe, delegate
            {
                var target = e.NewValue;
                d.SetValue(View.IsScopeRootProperty, true);

#if XFORMS || MAUI
                var context = fe.Id.ToString("N");
#else
                var context = string.IsNullOrEmpty(fe.Name)
                                  ? fe.GetHashCode().ToString()
                                  : fe.Name;
#endif

                d.SetValue(NoContextProperty, true);
                ViewModelBinder.Bind(target, d, context);
            });
        }

        /// <summary>
        /// Allows application of conventions at design-time.
        /// </summary>
        public static DependencyProperty AtDesignTimeProperty =
#if AVALONIA
            AvaloniaProperty.RegisterAttached<AvaloniaObject, bool>("AtDesignTime", typeof(Bind));
#else
            DependencyPropertyHelper.RegisterAttached(
                "AtDesignTime",
                typeof(bool),
                typeof(Bind),
                false,
                AtDesignTimeChanged);
#endif

        /// <summary>
        /// Gets whether or not conventions are being applied at design-time.
        /// </summary>
        /// <param name="dependencyObject">The ui to apply conventions to.</param>
        /// <returns>Whether or not conventions are applied.</returns>
#if (NET || CAL_NETCORE) && (!AVALONIA && !WINDOWS_UWP && !MAUI) && !WinUI3 // not sure this is right      
        [AttachedPropertyBrowsableForTypeAttribute(typeof(DependencyObject))]
#endif
        public static bool GetAtDesignTime(DependencyObject dependencyObject)
        {
            return (bool)dependencyObject.GetValue(AtDesignTimeProperty);
        }

        /// <summary>
        /// Sets whether or not do bind conventions at design-time.
        /// </summary>
        /// <param name="dependencyObject">The ui to apply conventions to.</param>
        /// <param name="value">Whether or not to apply conventions.</param>
        public static void SetAtDesignTime(DependencyObject dependencyObject, bool value)
        {
            dependencyObject.SetValue(AtDesignTimeProperty, value);
        }

        static void AtDesignTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!View.InDesignMode)
                return;

            var atDesignTime = (bool)e.NewValue;
            if (!atDesignTime)
                return;
#if XFORMS
            d.SetBinding(DataContextProperty, string.Empty);
#elif AVALONIA
            d.Bind(DataContextProperty, new Binding());
#elif MAUI
            d.SetBinding(DataContextProperty, null);
#else
            BindingOperations.SetBinding(d, DataContextProperty, new Binding());
#endif
        }

        static readonly DependencyProperty DataContextProperty =
#if AVALONIA
            AvaloniaProperty.RegisterAttached<AvaloniaObject, object>("DataContext", typeof(Bind));
#else
            DependencyPropertyHelper.RegisterAttached(
                "DataContext",
                typeof(object),
                typeof(Bind),
                null, DataContextChanged);
#endif

        static void DataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!View.InDesignMode)
                return;

            var enable = d.GetValue(AtDesignTimeProperty);
            if (enable == null || !((bool)enable) || e.NewValue == null)
                return;

            var fe = d as FrameworkElement;

            if (fe == null)
                return;
#if XFORMS || MAUI
            ViewModelBinder.Bind(e.NewValue, d, fe.Id.ToString("N"));
#else
            ViewModelBinder.Bind(e.NewValue, d, string.IsNullOrEmpty(fe.Name) ? fe.GetHashCode().ToString() : fe.Name);
#endif
        }
    }
}
