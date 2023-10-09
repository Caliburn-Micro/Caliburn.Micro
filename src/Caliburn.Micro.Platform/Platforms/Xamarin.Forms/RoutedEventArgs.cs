﻿using System;

namespace Caliburn.Micro.Xamarin.Forms
{
    /// <summary>
    /// Helper class with abstracting Xamarin Forms.
    /// </summary>
    public class RoutedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets source of the event.
        /// </summary>
        public object OriginalSource { get; set; }
    }
}
