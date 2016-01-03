using System;
using Caliburn.Micro;

namespace Features.CrossPlatform.ViewModels
{
    public class ConductorViewModel : Conductor<TabViewModel>.Collection.OneActive
    {
        private int count;

        public ConductorViewModel()
        {
            Items.CollectionChanged += (s, e) => NotifyOfPropertyChange(() => CanCloseTab);
        }

        protected override void OnInitialize()
        {
            AddTab();
            AddTab();
        }

        public void AddTab()
        {
            ActivateItem(new TabViewModel { DisplayName = $"Tab {count}" });

            count++;
        }

        public bool CanCloseTab => Items.Count > 1;

        public void CloseTab()
        {
            DeactivateItem(ActiveItem, close: true);
        }
    }
}
