namespace GameLibrary.ViewModels {
    using System.ComponentModel.Composition;
    using Caliburn.Micro;
    using Framework;

    [Export(typeof(NoResultsViewModel))]
    public class NoResultsViewModel {
        string searchText;

        public IResult AddGame() {
            return Show.Child<AddGameViewModel>()
                .In<IShell>()
                .Configured(x => x.Title = searchText);
        }

        public NoResultsViewModel WithTitle(string searchText) {
            this.searchText = searchText;
            return this;
        }
    }
}