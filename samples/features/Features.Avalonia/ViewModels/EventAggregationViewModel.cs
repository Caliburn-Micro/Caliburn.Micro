using System;
using Caliburn.Micro;
using Features.Avalonia.ViewModels.Events;

namespace Features.Avalonia.ViewModels
{
    public class EventAggregationViewModel : Screen
    {
        public EventAggregationViewModel(IEventAggregator eventAggregator)
        {
            Source = new EventSourceViewModel(eventAggregator);
            Destination = new EventDestinationViewModel(eventAggregator);
        }

        public EventSourceViewModel Source { get; }

        public EventDestinationViewModel Destination { get; }
    }
}
