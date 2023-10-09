#if WINDOWS_UWP
using System.Globalization;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
#elif XFORMS
using System;

using Xamarin.Forms;

using DependencyObject =Xamarin.Forms.BindableObject;
using DependencyProperty = Xamarin.Forms.BindableProperty;
using FrameworkElement = Xamarin.Forms.VisualElement;

#elif MAUI
using DependencyObject = Microsoft.Maui.Controls.BindableObject;
using DependencyProperty = Microsoft.Maui.Controls.BindableProperty;
using FrameworkElement = Microsoft.Maui.Controls.VisualElement;
#else
using System.Globalization;
using System.Windows;
using System.Windows.Data;
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
    ///   Hosts dependency properties for binding.
    /// </summary>
    public static class Bind {
        /// <summary>
        ///   Allows binding on an existing view. Use this on root UserControls, Pages and Windows; not in a DataTemplate.
        /// </summary>
        public static readonly DependencyProperty ModelProperty
            = DependencyPropertyHelper.RegisterAttached(
                "Model",
                typeof(object),
                typeof(Bind),
                null,
                ModelChanged);

        /// <summary>
        ///   Allows binding on an existing view without setting the data context. Use this from within a DataTemplate.
        /// </summary>
        public static readonly DependencyProperty ModelWithoutContextProperty
            = DependencyPropertyHelper.RegisterAttached(
                "ModelWithoutContext",
                typeof(object),
                typeof(Bind),
                null,
                ModelWithoutContextChanged);

        /// <summary>
        /// Allows application of conventions at design-time.
        /// </summary>
        public static readonly DependencyProperty AtDesignTimeProperty
            = DependencyPropertyHelper.RegisterAttached(
                "AtDesignTime",
                typeof(bool),
                typeof(Bind),
                false,
                AtDesignTimeChanged);

        internal static readonly DependencyProperty NoContextProperty
            = DependencyPropertyHelper.RegisterAttached(
                "NoContext",
                typeof(bool),
                typeof(Bind),
                false);

        private static readonly DependencyProperty DataContextProperty
            = DependencyPropertyHelper.RegisterAttached(
                "DataContext",
                typeof(object),
                typeof(Bind),
                null,
                DataContextChanged);

        /// <summary>
        ///   Gets the model to bind to.
        /// </summary>
        /// <param name = "dependencyObject">The dependency object to bind to.</param>
        /// <returns>The model.</returns>
        public static object GetModelWithoutContext(DependencyObject dependencyObject)
            => dependencyObject.GetValue(ModelWithoutContextProperty);

        /// <summary>
        ///   Sets the model to bind to.
        /// </summary>
        /// <param name = "dependencyObject">The dependency object to bind to.</param>
        /// <param name = "value">The model.</param>
        public static void SetModelWithoutContext(DependencyObject dependencyObject, object value)
            => dependencyObject.SetValue(ModelWithoutContextProperty, value);

        /// <summary>
        ///   Gets the model to bind to.
        /// </summary>
        /// <param name = "dependencyObject">The dependency object to bind to.</param>
        /// <returns>The model.</returns>
        public static object GetModel(DependencyObject dependencyObject) => dependencyObject.GetValue(ModelProperty);

        /// <summary>
        ///   Sets the model to bind to.
        /// </summary>
        /// <param name = "dependencyObject">The dependency object to bind to.</param>
        /// <param name = "value">The model.</param>
        public static void SetModel(DependencyObject dependencyObject, object value)
            => dependencyObject.SetValue(ModelProperty, value);

        /// <summary>
        /// Gets whether or not conventions are being applied at design-time.
        /// </summary>
        /// <param name="dependencyObject">The ui to apply conventions to.</param>
        /// <returns>Whether or not conventions are applied.</returns>
#if !MAUI && (NET || CAL_NETCORE)
        [AttachedPropertyBrowsableForTypeAttribute(typeof(DependencyObject))]
#endif
        public static bool GetAtDesignTime(DependencyObject dependencyObject)
            => (bool)dependencyObject.GetValue(AtDesignTimeProperty);

        /// <summary>
        /// Sets whether or not do bind conventions at design-time.
        /// </summary>
        /// <param name="dependencyObject">The ui to apply conventions to.</param>
        /// <param name="value">Whether or not to apply conventions.</param>
        public static void SetAtDesignTime(DependencyObject dependencyObject, bool value)
            => dependencyObject.SetValue(AtDesignTimeProperty, value);

        private static void ModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (View.InDesignMode || e.NewValue == null || e.NewValue == e.OldValue) {
                return;
            }

            if (!(d is FrameworkElement fe)) {
                return;
            }

            View.ExecuteOnLoad(fe, delegate {
                object target = e.NewValue;

                d.SetValue(View.IsScopeRootProperty, true);

#if XFORMS || MAUI
                string context = fe.Id.ToString("N");
#else
                string context = string.IsNullOrEmpty(fe.Name)
                                  ? fe.GetHashCode().ToString(CultureInfo.InvariantCulture)
                                  : fe.Name;
#endif

                ViewModelBinder.Bind(target, d, context);
            });
        }

        private static void ModelWithoutContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (View.InDesignMode || e.NewValue == null || e.NewValue == e.OldValue) {
                return;
            }

            if (!(d is FrameworkElement fe)) {
                return;
            }

            View.ExecuteOnLoad(fe, delegate {
                object target = e.NewValue;
                d.SetValue(View.IsScopeRootProperty, true);

#if XFORMS || MAUI
                string context = fe.Id.ToString("N");
#else
                string context = string.IsNullOrEmpty(fe.Name)
                                  ? fe.GetHashCode().ToString(CultureInfo.InvariantCulture)
                                  : fe.Name;
#endif

                d.SetValue(NoContextProperty, true);
                ViewModelBinder.Bind(target, d, context);
            });
        }

        private static void AtDesignTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (!View.InDesignMode) {
                return;
            }

            bool atDesignTime = (bool)e.NewValue;
            if (!atDesignTime) {
                return;
            }
#if XFORMS
            d.SetBinding(DataContextProperty, string.Empty);
#elif MAUI
            d.SetBinding(DataContextProperty, null);
#else
            BindingOperations.SetBinding(d, DataContextProperty, new Binding());
#endif
        }

        private static void DataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (!View.InDesignMode) {
                return;
            }

            object enable = d.GetValue(AtDesignTimeProperty);
            if (enable == null || ((bool)enable) == false || e.NewValue == null) {
                return;
            }

            if (!(d is FrameworkElement fe)) {
                return;
            }
#if XFORMS || MAUI
            ViewModelBinder.Bind(e.NewValue, d, fe.Id.ToString("N"));
#else
            ViewModelBinder.Bind(e.NewValue, d, string.IsNullOrEmpty(fe.Name) ? fe.GetHashCode().ToString(CultureInfo.InvariantCulture) : fe.Name);
#endif
        }
    }
}
