using System;
using DependencyObject = Microsoft.Maui.Controls.BindableObject;
using DependencyProperty = Microsoft.Maui.Controls.BindableProperty;

namespace Caliburn.Micro.Maui
{
    /// <summary>
    /// Helper type for abstracting differences between dependency / bindable properties.
    /// </summary>
    /// <param name="d">The dependency object</param>
    /// <param name="e">The property changed event args.</param>
    public delegate void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e);
}
