using System;
using Octokit;

namespace Caliburn.Micro.State.ViewModels
{
    public class RepositorySearchViewModel : PageViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IGitHubClient _gitHubClient;
        private string _term;

        public RepositorySearchViewModel(INavigationService navigationService, IGitHubClient gitHubClient)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _gitHubClient = gitHubClient;

            Repositories = new BindableCollection<Repository>();
        }

        public string Term
        {
            get { return _term; }
            set
            {
                _term = value;

                NotifyOfPropertyChange(() => Term);
                NotifyOfPropertyChange(() => CanSearch);
            }
        }

        public bool CanSearch
        {
            get { return !String.IsNullOrEmpty(Term); }
        }

        public async void Search()
        {
            Repositories.Clear();

            var repositories = await _gitHubClient.Search.SearchRepo(new SearchRepositoriesRequest(Term));

            Repositories.AddRange(repositories.Items);
        }

        public void RepositorySelected(Repository repository)
        {
            _navigationService.UriFor<RepositoryDetailsViewModel>()
                .WithParam(v => v.Owner, repository.Owner.Login)
                .WithParam(v => v.Name, repository.Name)
                .Navigate();
        }

        public BindableCollection<Repository> Repositories
        {
            get; private set;
        }
    }
}
