﻿using System;

namespace Caliburn.Micro.Maui
{
    /// <summary>
    /// Helper class with abstracting Xamarin Forms.
    /// </summary>
    public class RoutedEventArgs : EventArgs
    {
        /// <summary>
        /// Source of the event
        /// </summary>
        public object OriginalSource { get; set; }
    }
}
