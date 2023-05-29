﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Features.Avalonia.ViewModels
{
    public class ConductorViewModel : Conductor<TabViewModel>.Collection.OneActive
    {
        private int count;

        public ConductorViewModel()
        {
            Items.CollectionChanged += (s, e) => NotifyOfPropertyChange(() => CanCloseTab);
        }

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await AddTabAsync();
            await AddTabAsync();
        }

        public Task AddTabAsync()
        {
            return ActivateItemAsync(new TabViewModel { DisplayName = $"Tab {count++}" }, CancellationToken.None);
        }

	    public bool CanCloseTab => Items.Count > 1;

        public Task CloseTab()
        {
            return DeactivateItemAsync(ActiveItem, close: true, cancellationToken: CancellationToken.None);
        }
    }
}
