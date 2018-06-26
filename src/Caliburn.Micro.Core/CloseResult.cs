using System.Collections.Generic;

namespace Caliburn.Micro
{
    public class CloseResult<T> : ICloseResult<T>
    {
        public CloseResult(bool closeCanOccur, IEnumerable<T> children)
        {
            CloseCanOccur = closeCanOccur;
            Children = children;
        }
        
        public bool CloseCanOccur { get; }

        public IEnumerable<T> Children { get; }
    }
}
