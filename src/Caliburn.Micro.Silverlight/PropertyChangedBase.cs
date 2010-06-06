namespace Caliburn.Micro
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    public interface INotifyPropertyChangedEx : INotifyPropertyChanged
    {
        void NotifyOfPropertyChange(string propertyName);
    }

    public class PropertyChangedBase : INotifyPropertyChangedEx
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void NotifyOfPropertyChange(string propertyName)
        {
            Execute.OnUIThread(() => PropertyChanged(this, new PropertyChangedEventArgs(propertyName)));
        }

        public void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property)
        {
            NotifyOfPropertyChange(property.GetMemberInfo().Name);
        }
    }
}