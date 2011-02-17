namespace GameLibrary.ViewModels {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Composition;
    using System.ComponentModel.DataAnnotations;
    using System.Windows;
    using Caliburn.Micro;
    using Framework;
    using Model;
    using System.Linq;

    [Export(typeof(AddGameViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class AddGameViewModel : Screen, IDataErrorInfo {
        readonly IValidator validator; //NOTE: You could also achieve validation without implementing the IDataErrorInfo interface by using Caliburn's AOP support.
        string notes;
        double rating;
        string title;
        bool wasSaved;

        [ImportingConstructor]
        public AddGameViewModel(IValidator validator) {
            this.validator = validator;
        }

        [Required]
        public string Title {
            get { return title; }
            set {
                title = value;
                NotifyOfPropertyChange(() => Title);
                NotifyOfPropertyChange(() => CanAddGame);
            }
        }

        public string Notes {
            get { return notes; }
            set {
                notes = value;
                NotifyOfPropertyChange(() => Notes);
            }
        }

        public double Rating {
            get { return rating; }
            set {
                rating = value;
                NotifyOfPropertyChange(() => Rating);
            }
        }

        public bool CanAddGame {
            get { return string.IsNullOrEmpty(Error); }
        }

        public string this[string columnName] {
            get { return string.Join(Environment.NewLine, validator.Validate(this, columnName).Select(x => x.Message)); }
        }

        public string Error {
            get { return string.Join(Environment.NewLine, validator.Validate(this).Select(x => x.Message)); }
        }

        public IEnumerable<IResult> AddGame() {
            var add = new AddGameToLibrary {
                Title = Title,
                Notes = Notes,
                Rating = Rating
            }.AsResult();

            wasSaved = true;

            yield return add;
            yield return Show.Child<SearchViewModel>()
                .In<IShell>();
        }

        public override void CanClose(Action<bool> callback) {
            callback(
                wasSaved || MessageBox.Show(
                    "Are you sure you want to cancel?  Changes will be lost.",
                    "Unsaved Changes",
                    MessageBoxButton.OKCancel
                    ) == MessageBoxResult.OK
                );
        }
    }
}