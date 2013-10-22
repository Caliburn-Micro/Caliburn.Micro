using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Caliburn.Micro.WinRT.Sample
{
    public static class ViewModelHelper
    {
        public static bool Set<TProperty>(
            this INotifyPropertyChangedEx This,
            ref TProperty backingField,
            TProperty newValue,
            [CallerMemberName] string propertyName = null)
        {
            if (This == null)
                throw new ArgumentNullException("This");
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException("propertyName");

            if (EqualityComparer<TProperty>.Default.Equals(backingField, newValue))
                return false;

            backingField = newValue;
            This.NotifyOfPropertyChange(propertyName);
            return true;
        }
    }
}
