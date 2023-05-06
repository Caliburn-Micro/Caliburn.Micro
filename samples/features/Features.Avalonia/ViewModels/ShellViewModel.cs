using Caliburn.Micro;
using ColorTextBlock.Avalonia;
using System.Threading.Tasks;
using System.Threading;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avalonia.Controls;
using Features.CrossPlatform.ViewModels;

namespace Features.CrossPlatform.ViewModels
{
    public class ShellViewModel : Conductor<Screen>, IHandle<FeatureViewModel>
    {
        private readonly IEventAggregator _eventAggregator;

        public ShellViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);
        }

        public async Task HandleAsync(FeatureViewModel message, CancellationToken cancellationToken)
        {
            var vm = IoC.GetInstance(message.ViewModel, null) as Screen;
            await ActivateItemAsync(vm);
        }

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);

            await GoHome();
        }

        public async Task<bool> GoHome()
        {
            var menuVM = IoC.GetInstance(typeof(MenuViewModel), null) as MenuViewModel;
            await ActivateItemAsync(menuVM);
            return true;
        }
    }

}
