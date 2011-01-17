namespace GameLibrary.ViewModels {
    using System.ComponentModel.Composition;
    using Caliburn.Micro;
    using Model;

    [Export(typeof(ExploreGameViewModel))]
    public class ExploreGameViewModel : Screen {
        string borrower;
        GameDTO game;

        public GameDTO Game {
            get { return game; }
        }

        public string BorrowedMessage {
            get { return Game.Title + " is currently checked out to " + Game.Borrower + "."; }
        }

        public string Borrower {
            get { return borrower; }
            set {
                borrower = value;
                NotifyOfPropertyChange(() => Borrower);
                NotifyOfPropertyChange(() => CanCheckOut);
            }
        }

        public bool IsCheckedOut {
            get { return !string.IsNullOrEmpty(game.Borrower); }
        }

        public bool IsCheckedIn {
            get { return !IsCheckedOut; }
        }

        public bool CanCheckOut {
            get { return !string.IsNullOrEmpty(Borrower); }
        }

        public void WithGame(GameDTO game) {
            this.game = game;
            Borrower = game.Borrower ?? string.Empty;
            Refresh();
        }

        public IResult CheckIn() {
            SetBorrower(null);

            return new CheckGameIn {
                Id = game.Id
            }.AsResult();
        }

        public IResult CheckOut() {
            SetBorrower(Borrower);

            return new CheckGameOut {
                Id = game.Id,
                Borrower = Borrower
            }.AsResult();
        }

        void SetBorrower(string borrower) {
            game.Borrower = borrower;
            Borrower = borrower;

            NotifyOfPropertyChange(() => IsCheckedOut);
            NotifyOfPropertyChange(() => IsCheckedIn);
            NotifyOfPropertyChange(() => BorrowedMessage);
        }
    }
}