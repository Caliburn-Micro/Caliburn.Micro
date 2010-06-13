namespace Caliburn.Micro
{
    using System;

    public class ResultCompletionEventArgs : EventArgs
    {
        public Exception Error;
        public bool WasCancelled;
    }
}