namespace GameLibrary.Model {
    using System.Collections.Generic;

    public class SearchGames : IQuery<IEnumerable<SearchResult>> {
        public string SearchText { get; set; }
    }
}