namespace GameLibrary.Framework {
    using System;

    public static class ResultExtensions {
        public static IOpenResult<TChild> Configured<TChild>(this IOpenResult<TChild> result, Action<TChild> configure) {
            result.OnConfigure = configure;
            return result;
        }

        public static IOpenResult<TChild> WhenClosing<TChild>(this IOpenResult<TChild> result, Action<TChild> onShutdown) {
            result.OnClose = onShutdown;
            return result;
        }
    }
}