using System;
using Windows.ApplicationModel;

namespace Caliburn.Micro.HelloUWP.Messages
{
    public class SuspendStateMessage
    {
        public SuspendStateMessage(SuspendingOperation operation)
        {
            Operation = operation;
        }

        public SuspendingOperation Operation { get; }
    }
}
