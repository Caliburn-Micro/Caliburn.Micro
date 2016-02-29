using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Caliburn.Micro;

namespace Hello.Xamarin.Core.Framework
{
    public static class NotifyPropertyChangedExtensions
    {
        public static void OnChanged<TViewModel, TProperty>(this TViewModel viewModel, Expression<Func<TViewModel, TProperty>> property, Action onChanged)
            where TViewModel : INotifyPropertyChanged
        {
            var name = property.GetMemberInfo().Name;

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName != name)
                    return;

                onChanged();
            };
        }
    }
}
