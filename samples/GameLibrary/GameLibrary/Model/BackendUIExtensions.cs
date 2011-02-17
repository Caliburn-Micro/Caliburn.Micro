namespace GameLibrary.Model {
    public static class BackendUIExtensions {
        public static QueryResult<TResponse> AsResult<TResponse>(this IQuery<TResponse> query) {
            return new QueryResult<TResponse>(query);
        }

        public static CommandResult AsResult(this ICommand command) {
            return new CommandResult(command);
        }
    }
}