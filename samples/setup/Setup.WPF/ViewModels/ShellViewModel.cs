using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;
using Caliburn.Micro;

namespace Setup.WPF.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<string>
    {
        private string _message;

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                NotifyOfPropertyChange("Message");
            }
        }


        private IEventAggregator _eventAggregator;
        public ShellViewModel()
        {
            _eventAggregator = new EventAggregator();
            _eventAggregator.SubscribeOnUIThread(this);
            Task.Run(async () =>
            {
                await ActivateItemAsync(new MainViewModel(_eventAggregator));
            });
        }


        public Task HandleAsync(string message, CancellationToken cancellationToken)
        {
            this.Message = message.ToString();
            return Task.CompletedTask;
        }


    }
}
