namespace GameLibrary.Model {
    using System;

    public class GameDTO {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
        public string Notes { get; set; }
        public string Borrower { get; set; }
        public DateTime AddedOn { get; set; }
    }
}