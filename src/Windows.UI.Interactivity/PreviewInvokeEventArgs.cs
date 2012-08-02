using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.UI.Interactivity
{
    /// <summary>
    /// Argument passed to PreviewInvoke event. Assigning Cancelling to True will cancel the invoking of the trigger.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// This is an infrastructure class. Behavior attached to a trigger base object can add its behavior as a listener to TriggerBase.PreviewInvoke.
    /// </remarks>
    public class PreviewInvokeEventArgs : EventArgs
    {
        public bool Cancelling { get; set; }
    }
}
