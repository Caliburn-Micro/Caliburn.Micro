namespace Caliburn.Micro
{
    using System.Reflection;

    /// <summary>
    /// Specifies how to save/load a particular class or property.
    /// </summary>
    public interface ITombstone
    {
        /// <summary>
        /// Tombstones the class/property.
        /// </summary>
        /// <param name="phoneService">An instance of the phone service.</param>
        /// <param name="owner">The owner of the property or null if it is the root.</param>
        /// <param name="property">The property to persist or null if the value is the root.</param>
        /// <param name="value">The value to persist.</param>
        /// <param name="rootKey">The key to use for persistance.</param>
        void Tombstone(IPhoneService phoneService, object owner, PropertyInfo property, object value, string rootKey);

        /// <summary>
        /// Resurrects the class/property.
        /// </summary>
        /// <param name="phoneService">An instance of the phone service.</param>
        /// <param name="owner">The owner of the property or null if it is the root.</param>
        /// <param name="property">The property to resurrect or null if the value is the root.</param>
        /// <param name="value">The value to resurrect.</param>
        /// <param name="rootKey">The key to use for resurrection.</param>
        void Resurrect(IPhoneService phoneService, object owner, PropertyInfo property, object value, string rootKey);
    }
}