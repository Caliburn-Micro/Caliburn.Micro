using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.UI.Interactivity
{
    /// <summary>
    /// Specifies type constraints on the AssociatedObject of TargetedTriggerAction and EventTriggerBase.
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class TypeConstraintAttribute : Attribute
    {
        /// <summary>
        /// Gets the constraint type.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The constraint type.
        /// </value>
        public Type Constraint { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Interactivity.TypeConstraintAttribute"/> class.
        /// 
        /// </summary>
        /// <param name="constraint">The constraint type.</param>
        public TypeConstraintAttribute(Type constraint)
        {
            this.Constraint = constraint;
        }
    }
}
