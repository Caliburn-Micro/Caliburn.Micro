using System;
using Octokit;

namespace Caliburn.Micro.State.ViewModels
{
    public class RepositoryDetailsViewModel : PageViewModelBase
    {
        private readonly IGitHubClient _gitHubClient;
        private Repository _repository;

        public RepositoryDetailsViewModel(INavigationService navigationService, IGitHubClient gitHubClient)
            : base(navigationService)
        {
            _gitHubClient = gitHubClient;
        }

        protected override async void OnInitialize()
        {
            Repository = await _gitHubClient.Repository.Get(Owner, Name);
        }

        public string Owner
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public Repository Repository
        {
            get { return _repository; }
            set
            {
                _repository = value;
                NotifyOfPropertyChange(() => Repository);
            }
        }
    }
}
