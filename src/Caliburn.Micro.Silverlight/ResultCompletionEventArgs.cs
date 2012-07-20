namespace Caliburn.Micro {
    using System;

    /// <summary>
    /// The event args for the Completed event of an <see cref="IResult"/>.
    /// </summary>
    public class ResultCompletionEventArgs : EventArgs {
        /// <summary>
        /// Gets or sets the error if one occurred.
        /// </summary>
        /// <value>The error.</value>
        public Exception Error;

        /// <summary>
        /// Gets or sets a value indicating whether the result was cancelled.
        /// </summary>
        /// <value><c>true</c> if cancelled; otherwise, <c>false</c>.</value>
        public bool WasCancelled;
    }
}