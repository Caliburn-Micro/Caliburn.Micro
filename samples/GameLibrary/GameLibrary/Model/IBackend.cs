namespace GameLibrary.Model {
    using System;

    public interface IBackend {
        void Send(ICommand command);
        void Send<TResponse>(IQuery<TResponse> query, Action<TResponse> reply);
    }
}