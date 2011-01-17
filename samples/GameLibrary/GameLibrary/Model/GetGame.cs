namespace GameLibrary.Model {
    using System;

    public class GetGame : IQuery<GameDTO> {
        public Guid Id { get; set; }
    }
}