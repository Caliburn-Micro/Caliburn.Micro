namespace GameLibrary.Model {
    using System;

    public class CheckGameOut : ICommand {
        public Guid Id { get; set; }
        public string Borrower { get; set; }
    }
}