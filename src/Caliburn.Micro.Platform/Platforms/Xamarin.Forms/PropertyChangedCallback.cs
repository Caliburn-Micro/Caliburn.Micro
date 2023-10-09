﻿using DependencyObject = Xamarin.Forms.BindableObject;

namespace Caliburn.Micro.Xamarin.Forms
{
    /// <summary>
    /// Helper type for abstracting differences between dependency / bindable properties.
    /// </summary>
    /// <param name="d">The dependency object.</param>
    /// <param name="e">The property changed event args.</param>
    public delegate void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e);
}
