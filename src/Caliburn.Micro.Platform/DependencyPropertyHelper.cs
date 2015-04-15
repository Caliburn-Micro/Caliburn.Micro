using System;
#if XFORMS
using Xamarin.Forms;
using DependencyProperty = Xamarin.Forms.BindableProperty;
#elif WinRT
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace Caliburn.Micro
{
    public static class DependencyPropertyHelper
    {

        public static DependencyProperty RegisterAttached(string name, Type propertyType, Type ownerType, object defaultValue = null, PropertyChangedCallback propertyChangedCallback = null) {
#if XFORMS
            return DependencyProperty.CreateAttached(name, propertyType, ownerType, defaultValue, propertyChanged: (obj, oldValue, newValue) => {
                if (propertyChangedCallback != null)
                    propertyChangedCallback(obj, new DependencyPropertyChangedEventArgs(newValue, oldValue, null));
            });
#else
            return DependencyProperty.RegisterAttached(name, propertyType, ownerType, new PropertyMetadata(defaultValue, propertyChangedCallback));
#endif
        }

        public static DependencyProperty Register(string name, Type propertyType, Type ownerType, object defaultValue = null, PropertyChangedCallback propertyChangedCallback = null)
        {
#if XFORMS
            return DependencyProperty.Create(name, propertyType, ownerType, defaultValue, propertyChanged: (obj, oldValue, newValue) =>
            {
                if (propertyChangedCallback != null)
                    propertyChangedCallback(obj, new DependencyPropertyChangedEventArgs(newValue, oldValue, null));
            });
#else
            return DependencyProperty.Register(name, propertyType, ownerType, new PropertyMetadata(defaultValue, propertyChangedCallback));
#endif
        }
    }
}
