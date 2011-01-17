namespace GameLibrary.Model {
    using System;

    public class CheckGameIn : ICommand {
        public Guid Id { get; set; }
    }
}