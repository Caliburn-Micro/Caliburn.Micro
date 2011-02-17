namespace GameLibrary.ViewModels {
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using Caliburn.Micro;
    using Framework;
    using Model;

    [Export(typeof(SearchViewModel))]
    public class SearchViewModel : Screen {
        readonly NoResultsViewModel noResults;
        readonly ResultsViewModel results;

        object searchResults;
        string searchText;

        [ImportingConstructor]
        public SearchViewModel(NoResultsViewModel noResults, ResultsViewModel results) {
            this.noResults = noResults;
            this.results = results;
        }

        public string SearchText {
            get { return searchText; }
            set {
                searchText = value;
                NotifyOfPropertyChange(() => SearchText);
                NotifyOfPropertyChange(() => CanExecuteSearch);
            }
        }

        public object SearchResults {
            get { return searchResults; }
            set {
                searchResults = value;
                NotifyOfPropertyChange(() => SearchResults);
            }
        }

        public bool CanExecuteSearch {
            get { return !string.IsNullOrEmpty(SearchText); }
        }

        public IEnumerable<IResult> ExecuteSearch() {
            var search = new SearchGames {
                SearchText = SearchText
            }.AsResult();

            yield return Show.Busy();
            yield return search;

            var resultCount = search.Response.Count();

            if(resultCount == 0)
                SearchResults = noResults.WithTitle(SearchText);
            else if(resultCount == 1 && search.Response.First().Title == SearchText) {
                var getGame = new GetGame {
                    Id = search.Response.First().Id
                }.AsResult();

                yield return getGame;
                yield return Show.Child<ExploreGameViewModel>()
                    .In<IShell>()
                    .Configured(x => x.WithGame(getGame.Response));
            }
            else SearchResults = results.With(search.Response);

            yield return Show.NotBusy();
        }

        public IResult AddGame() {
            return Show.Child<AddGameViewModel>()
                .In<IShell>()
                .Configured(x => x.Title = "New Game");
        }

        protected override void OnActivate() {
            SearchText = null;
            SearchResults = null;

            base.OnActivate();
        }
    }
}