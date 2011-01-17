namespace GameLibrary.Model {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;
    using System.Threading;

    [Export(typeof(IBackend))]
    public class FakeBackend : IBackend {
        //The 'Notes' are taken from the Game Spot reviews for each of these games.
        readonly List<GameDTO> games = new List<GameDTO> {
            new GameDTO {
                Id = Guid.NewGuid(),
                Title = "Halo 1",
                Rating = 1,
                Notes = "Not only is this easily the best of the Xbox launch games, but it's easily one of the best shooters ever, on any platform.",
                AddedOn = DateTime.Today
            },
            new GameDTO {
                Id = Guid.NewGuid(),
                Title = "Halo 2",
                Rating = .8,
                Notes = "Despite a rather short campaign and a disappointing storyline, Halo 2 is an exceptional shooter that frequently delivers thrilling, memorable, and unique moments in its online, co-op, and single-player modes.",
                AddedOn = DateTime.Today
            },
            new GameDTO {
                Id = Guid.NewGuid(),
                Title = "Halo 3",
                Rating = .8,
                Notes = "Halo 3 builds upon the concepts of Halo 2 in ways that you'd expect, but there are also new modes and options that send the series in exciting new directions.",
                AddedOn = DateTime.Today
            },
            new GameDTO {
                Id = Guid.NewGuid(),
                Title = "Mass Effect 1",
                Rating = .8,
                Notes = "An excellent story and fun battles make this a universe worth exploring.",
                AddedOn = DateTime.Today
            },
            new GameDTO {
                Id = Guid.NewGuid(),
                Title = "Mass Effect 2",
                Rating = 1,
                Notes = "Once this intense and action-packed role-playing game pulls you into its orbit, you won't want to escape.",
                AddedOn = DateTime.Today
            },
            new GameDTO {
                Id = Guid.NewGuid(),
                Title = "Final Fantasy XIII",
                Rating = .8,
                Notes = "The most beautiful Final Fantasy game yet is an imperfect but still impressive saga that will touch your heart.",
                AddedOn = DateTime.Today
            }
        };

        readonly IEnumerable<MethodInfo> methods = typeof(FakeBackend)
            .GetMethods()
            .Where(x => x.Name == "Handle");

        public void Send<TResponse>(IQuery<TResponse> query, Action<TResponse> reply) {
            Invoke(query, query, reply);
        }

        public void Send(ICommand command) {
            Invoke(command, command);
        }

        void Invoke(object request, params object[] args) {
            ThreadPool.QueueUserWorkItem(state => {
                Thread.Sleep(1000); //simulating network

                var requestType = request.GetType();
                var handler = methods.Where(x => requestType.IsAssignableFrom(x.GetParameters().First().ParameterType)).First();

                handler.Invoke(this, args);
            });
        }

        public void Handle(SearchGames search, Action<IEnumerable<SearchResult>> reply) {
            reply(
                from game in games
                where game.Title.ToLower().Contains(search.SearchText.ToLower())
                orderby game.Title
                select new SearchResult {
                    Id = game.Id,
                    Title = game.Title
                });
        }

        public void Handle(GetGame getGame, Action<GameDTO> reply) {
            reply(
                (from game in games
                 where game.Id == getGame.Id
                 select game).FirstOrDefault()
                );
        }

        public void Handle(AddGameToLibrary addGame) {
            var game = new GameDTO {
                Id = Guid.NewGuid(),
                Title = addGame.Title,
                Notes = addGame.Notes,
                Rating = addGame.Rating,
                AddedOn = DateTime.Now,
            };

            games.Add(game);
        }

        public void Handle(CheckGameIn checkIn) {
            var game = games.FirstOrDefault(x => x.Id == checkIn.Id);
            if(game != null)
                game.Borrower = null;
        }

        public void Handle(CheckGameOut checkOut) {
            var game = games.FirstOrDefault(x => x.Id == checkOut.Id);
            if(game != null)
                game.Borrower = checkOut.Borrower;
        }
    }
}