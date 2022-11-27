using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ReactiveUI;

namespace Caliburn.Micro
{
    public class ReactiveScreen : Screen, IRoutableViewModel
    {
        public RoutingState Router { get; } = new RoutingState();

        public string UrlPathSegment { get; } = Guid.NewGuid().ToString();
        //add backing field
        private ReactiveUI.IScreen _HostScreen;
        public ReactiveUI.IScreen HostScreen
        {
            get
            {
                return _HostScreen;
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public void RaisePropertyChanged(PropertyChangedEventArgs args)
        {
            NotifyOfPropertyChange(args.PropertyName);
        }

        public void RaisePropertyChanging(PropertyChangingEventArgs args)
        {
            PropertyChanging?.Invoke(this, args);
        }

        public void SetHostScreen(ReactiveUI.IScreen host)
        {
            _HostScreen = host;
        }
    }

}

