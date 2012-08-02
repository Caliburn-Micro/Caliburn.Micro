using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.UI.Interactivity
{
    /// <summary>
    /// Associates the given editor type with the property on which the CustomPropertyValueEditor is applied.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// Use this attribute to get improved design-time editing for properties that denote element (by name), storyboards, or states (by name).
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class CustomPropertyValueEditorAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the custom property value editor.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The custom property value editor.
        /// </value>
        public CustomPropertyValueEditor CustomPropertyValueEditor { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Interactivity.CustomPropertyValueEditorAttribute"/> class.
        /// 
        /// </summary>
        /// <param name="customPropertyValueEditor">The custom property value editor.</param>
        public CustomPropertyValueEditorAttribute(CustomPropertyValueEditor customPropertyValueEditor)
        {
            this.CustomPropertyValueEditor = customPropertyValueEditor;
        }
    }
}
