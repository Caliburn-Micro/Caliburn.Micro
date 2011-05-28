using System.Collections.Generic;

namespace Caliburn.Micro
{
    /// <summary>
    /// Class representing a convention for doing name resolution.
    /// </summary>
    public class NameTransformConvention
    {
        /// <summary>
        /// A global regular expression pattern to used to filter conventions in a list of NameTransformConventions.
        /// Empty or null means that convention is always included as a possible candidate.
        /// </summary>
        public string GlobalFilterPattern { get; set; }
        
        /// <summary>
        /// The regular expression pattern used for replacing text when transforming a name into a resolved name.
        /// </summary>
        public string ReplacePattern { get; set; }
        
        /// <summary>
        /// The list of values that will applied to the replace pattern in order get a list of resolved names.
        /// </summary>
        public IEnumerable<string> ReplaceValueList { get; set; }
    }
}
