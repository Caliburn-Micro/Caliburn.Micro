using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.UI.Interactivity
{
    /// <summary>
    /// Provides data about which objects were affected when resolving a name change.
    /// 
    /// </summary>
    internal sealed class NameResolvedEventArgs : EventArgs
    {
        private object oldObject;
        private object newObject;

        public object OldObject
        {
            get
            {
                return this.oldObject;
            }
        }

        public object NewObject
        {
            get
            {
                return this.newObject;
            }
        }

        public NameResolvedEventArgs(object oldObject, object newObject)
        {
            this.oldObject = oldObject;
            this.newObject = newObject;
        }
    }
}
