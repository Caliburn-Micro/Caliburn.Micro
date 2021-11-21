
namespace Caliburn.Micro
{
    using System;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Data;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;



    /// <summary>
    ///   Hosts dependency properties for binding.
    /// </summary>
    public static class Bind {
        /// <summary>
        ///   Allows binding on an existing view. Use this on root UserControls, Pages and Windows; not in a DataTemplate.
        /// </summary>
        public static DependencyProperty ModelProperty =
            DependencyPropertyHelper.RegisterAttached(
                "Model",
                typeof(object),
                typeof(Bind),
                null, 
                ModelChanged);

        /// <summary>
        ///   Allows binding on an existing view without setting the data context. Use this from within a DataTemplate.
        /// </summary>
        public static DependencyProperty ModelWithoutContextProperty =
            DependencyPropertyHelper.RegisterAttached(
                "ModelWithoutContext",
                typeof(object),
                typeof(Bind),
                null, 
                ModelWithoutContextChanged);

        internal static DependencyProperty NoContextProperty =
            DependencyPropertyHelper.RegisterAttached(
                "NoContext",
                typeof(bool),
                typeof(Bind),
                false);

        /// <summary>
        ///   Gets the model to bind to.
        /// </summary>
        /// <param name = "dependencyObject">The dependency object to bind to.</param>
        /// <returns>The model.</returns>
        public static object GetModelWithoutContext(DependencyObject dependencyObject) {
            return dependencyObject.GetValue(ModelWithoutContextProperty);
        }

        /// <summary>
        ///   Sets the model to bind to.
        /// </summary>
        /// <param name = "dependencyObject">The dependency object to bind to.</param>
        /// <param name = "value">The model.</param>
        public static void SetModelWithoutContext(DependencyObject dependencyObject, object value) {
            dependencyObject.SetValue(ModelWithoutContextProperty, value);
        }

        /// <summary>
        ///   Gets the model to bind to.
        /// </summary>
        /// <param name = "dependencyObject">The dependency object to bind to.</param>
        /// <returns>The model.</returns>
        public static object GetModel(DependencyObject dependencyObject) {
            return dependencyObject.GetValue(ModelProperty);
        }

        /// <summary>
        ///   Sets the model to bind to.
        /// </summary>
        /// <param name = "dependencyObject">The dependency object to bind to.</param>
        /// <param name = "value">The model.</param>
        public static void SetModel(DependencyObject dependencyObject, object value) {
            dependencyObject.SetValue(ModelProperty, value);
        }

        static void ModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (View.InDesignMode || e.NewValue == null || e.NewValue == e.OldValue) {
                return;
            }

            var fe = d as FrameworkElement;
            if (fe == null) {
                return;
            }

            View.ExecuteOnLoad(fe, delegate {
                var target = e.NewValue;

                d.SetValue(View.IsScopeRootProperty, true);


                var context = string.IsNullOrEmpty(fe.Name)
                                  ? fe.GetHashCode().ToString()
                                  : fe.Name;

                ViewModelBinder.Bind(target, d, context);
            });
        }

        static void ModelWithoutContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (View.InDesignMode || e.NewValue == null || e.NewValue == e.OldValue) {
                return;
            }

            var fe = d as FrameworkElement;
            if (fe == null) {
                return;
            }

            View.ExecuteOnLoad(fe, delegate {
                var target = e.NewValue;
                d.SetValue(View.IsScopeRootProperty, true);


                var context = string.IsNullOrEmpty(fe.Name)
                                  ? fe.GetHashCode().ToString()
                                  : fe.Name;

                d.SetValue(NoContextProperty, true);
                ViewModelBinder.Bind(target, d, context);
            });
        }

        /// <summary>
        /// Allows application of conventions at design-time.
        /// </summary>
        public static DependencyProperty AtDesignTimeProperty =
            DependencyPropertyHelper.RegisterAttached(
                "AtDesignTime",
                typeof(bool),
                typeof(Bind),
                false, 
                AtDesignTimeChanged);

        /// <summary>
        /// Gets whether or not conventions are being applied at design-time.
        /// </summary>
        /// <param name="dependencyObject">The ui to apply conventions to.</param>
        /// <returns>Whether or not conventions are applied.</returns>

        public static bool GetAtDesignTime(DependencyObject dependencyObject) {
            return (bool)dependencyObject.GetValue(AtDesignTimeProperty);
        }

        /// <summary>
        /// Sets whether or not do bind conventions at design-time.
        /// </summary>
        /// <param name="dependencyObject">The ui to apply conventions to.</param>
        /// <param name="value">Whether or not to apply conventions.</param>
        public static void SetAtDesignTime(DependencyObject dependencyObject, bool value) {
            dependencyObject.SetValue(AtDesignTimeProperty, value);
        }

        static void AtDesignTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (!View.InDesignMode)
                return;

            var atDesignTime = (bool) e.NewValue;
            if (!atDesignTime)
                return;

            BindingOperations.SetBinding(d, DataContextProperty, new Binding());
        }

        static readonly DependencyProperty DataContextProperty =
            DependencyPropertyHelper.RegisterAttached(
                "DataContext",
                typeof(object),
                typeof(Bind),
                null, DataContextChanged);

        static void DataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (!View.InDesignMode)
                return;

            var enable = d.GetValue(AtDesignTimeProperty);
            if (enable == null || ((bool)enable) == false || e.NewValue == null)
                return;

            var fe = d as FrameworkElement;
            if (fe == null)
                return;

            ViewModelBinder.Bind(e.NewValue, d, string.IsNullOrEmpty(fe.Name) ? fe.GetHashCode().ToString() : fe.Name);
        }
    }
}
