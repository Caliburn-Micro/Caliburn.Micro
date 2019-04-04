using System;
using DependencyObject = Xamarin.Forms.BindableObject;
using DependencyProperty = Xamarin.Forms.BindableProperty;

namespace Caliburn.Micro
{
    /// <summary>
    /// Helper type for abstracting differences between dependency / bindable properties.
    /// </summary>
    /// <param name="d">The dependency object</param>
    /// <param name="e">The property changed event args.</param>
    public delegate void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e);
}
