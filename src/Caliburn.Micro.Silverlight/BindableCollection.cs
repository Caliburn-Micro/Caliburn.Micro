namespace Caliburn.Micro
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;

    public interface IObservableCollection<T> : IList<T>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        void AddRange(IEnumerable<T> items);
    }

    public class BindableCollection<T> : ObservableCollection<T>, IObservableCollection<T>
    {
        private bool raiseCollectionChanged = true;

        public BindableCollection() { }

        public BindableCollection(IEnumerable<T> collection)
        {
            AddRange(collection);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            Execute.OnUIThread(() => RaisePropertyChangedEventImmediately(e));
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (raiseCollectionChanged)
                Execute.OnUIThread(() => RaiseCollectionChangedEventImmediately(e));
        }

        public void RaiseCollectionChangedEventImmediately(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
        }

        public void RaisePropertyChangedEventImmediately(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
        }

        public void AddRange(IEnumerable<T> items)
        {
            raiseCollectionChanged = false;
            items.Apply(Add);
            raiseCollectionChanged = true;

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}