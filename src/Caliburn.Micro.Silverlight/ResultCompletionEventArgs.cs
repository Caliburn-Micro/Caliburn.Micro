namespace Caliburn.Micro
{
    using System;

    public class ResultCompletionEventArgs : EventArgs
    {
        public Exception Error { get; set; }
        public bool WasCancelled { get; set; }
    }
}