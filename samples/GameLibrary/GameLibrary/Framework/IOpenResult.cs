namespace GameLibrary.Framework {
    using System;
    using Caliburn.Micro;

    public interface IOpenResult<TTarget> : IResult {
        Action<TTarget> OnConfigure { get; set; }
        Action<TTarget> OnClose { get; set; }
    }
}