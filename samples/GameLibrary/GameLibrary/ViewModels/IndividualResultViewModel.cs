namespace GameLibrary.ViewModels {
    using System.Collections.Generic;
    using Caliburn.Micro;
    using Framework;
    using Model;

    public class IndividualResultViewModel : Screen {
        readonly int number;
        readonly SearchResult result;

        public IndividualResultViewModel(SearchResult result, int number) {
            this.result = result;
            this.number = number;
        }

        public int Number {
            get { return number; }
        }

        public string Title {
            get { return result.Title; }
        }

        public IEnumerable<IResult> Open() {
            var getGame = new GetGame {
                Id = result.Id
            }.AsResult();

            yield return Show.Busy();
            yield return getGame;
            yield return Show.Child<ExploreGameViewModel>()
                .In<IShell>()
                .Configured(x => x.WithGame(getGame.Response));
            yield return Show.NotBusy();
        }
    }
}