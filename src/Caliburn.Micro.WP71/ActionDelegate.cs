namespace System {
    /// <summary>
    /// Encapsulates a method that has five type parameters and does not return a value.
    /// </summary>
    /// <typeparam name="T1">The first type parameter.</typeparam>
    /// <typeparam name="T2">The second type parameter.</typeparam>
    /// <typeparam name="T3">The thrid type parameter.</typeparam>
    /// <typeparam name="T4">The fourth type parameter.</typeparam>
    /// <typeparam name="T5">The fifth type parameter.</typeparam>
    /// <typeparam name="T6">The sixth type parameter.</typeparam>
    public delegate void Action<T1, T2, T3, T4, T5, T6>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6);

    /// <summary>
    /// Encapsulates a method that has five type parameters and returns a value.
    /// </summary>
    /// <typeparam name="T1">The first type parameter.</typeparam>
    /// <typeparam name="T2">The second type parameter.</typeparam>
    /// <typeparam name="T3">The thrid type parameter.</typeparam>
    /// <typeparam name="T4">The fourth type parameter.</typeparam>
    /// <typeparam name="T5">The fifth type parameter.</typeparam>
    /// <typeparam name="TResult">The return type.</typeparam>
    public delegate TResult Func<T1, T2, T3, T4, T5, TResult>(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5);
}