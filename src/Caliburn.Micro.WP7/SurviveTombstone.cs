namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// A basic implementation of <see cref="ITombstone"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class SurviveTombstone : Attribute, ITombstone
    {
        ///<summary>
        /// Indicates that the entire value of the attributed property should be stored in the phone state.
        ///</summary>
        public bool SerializeComplexType { get; set; }

        /// <summary>
        /// Tombstones the class/property.
        /// </summary>
        /// <param name="phoneService">An instance of the phone service.</param>
        /// <param name="owner">The owner of the property or null if it is the root.</param>
        /// <param name="property">The property to persist or null if the value is the root.</param>
        /// <param name="value">The value to persist.</param>
        /// <param name="rootKey">The key to use for persistance.</param>
        public virtual void Tombstone(IPhoneService phoneService, object owner, PropertyInfo property, object value, string rootKey)
        {
            if(value == null)
                return;

            var type = value.GetType();

            if(IsComplexType(type))
            {
                var key = GetStateKey(value, rootKey);
                var conductor = value as IConductActiveItem;
                if(conductor != null)
                {
                    var items = conductor.GetChildren().OfType<object>().ToList();
                    phoneService.State[key + "|ActiveItemIndex"] = items.IndexOf(conductor.ActiveItem);

                    GetPersistableItems(items, key)
                        .Apply(x => x.Item2.Tombstone(phoneService, value, null, x.Item1, x.Item3));
                }

                GetPersistableProperties(value, key)
                    .Apply(x => x.Item2.Tombstone(phoneService, value, x.Item1, x.Item1.GetValue(value, null), x.Item3));
            }
            else phoneService.State[rootKey] = value;
        }

        /// <summary>
        /// Resurrects the class/property.
        /// </summary>
        /// <param name="phoneService">An instance of the phone service.</param>
        /// <param name="owner">The owner of the property or null if it is the root.</param>
        /// <param name="property">The property to resurrect or null if the value is the root.</param>
        /// <param name="value">The value to resurrect.</param>
        /// <param name="rootKey">The key to use for resurrection.</param>
        public virtual void Resurrect(IPhoneService phoneService, object owner, PropertyInfo property, object value, string rootKey)
        {
            var type = property == null ? value.GetType() : property.PropertyType;

            if(IsComplexType(type))
            {
                if(value == null)
                    return;

                var key = GetStateKey(value, rootKey);

                var conductor = value as IConductActiveItem;
                if(conductor != null)
                {
                    var items = conductor.GetChildren().OfType<object>().ToList();
                    var conductorId = key + "|ActiveItemIndex";

                    if(phoneService.State.ContainsKey(conductorId))
                    {
                        var index = Convert.ToInt32(phoneService.State[conductorId]);

                        if (index > 0 && index < items.Count)
                            conductor.ActivateItem(items[index]);
                    }
                    else
                        conductor.ActivateItem(items.FirstOrDefault());

                    GetPersistableItems(items, key)
                        .Apply(x => x.Item2.Resurrect(phoneService, value, null, x.Item1, x.Item3));
                }

                GetPersistableProperties(value, key)
                    .Apply(x => x.Item2.Resurrect(
                        phoneService,
                        value,
                        x.Item1,
                        IsComplexType(x.Item1.PropertyType) ? x.Item1.GetValue(value, null) : null,
                        x.Item3
                        )
                    );
            }
            else
            {
                object persisted;
                if(phoneService.State.TryGetValue(rootKey, out persisted))
                    property.SetValue(owner, persisted, null);
            }
        }

        /// <summary>
        /// Determines if the type is a complex type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual bool IsComplexType(Type type)
        {
            return !SerializeComplexType && (type.IsClass || type.IsInterface) && !typeof(string).IsAssignableFrom(type);
        }

        /// <summary>
        /// Returns the persistable items from the collection.
        /// </summary>
        /// <param name="items">The items to inspect.</param>
        /// <param name="key">The root persistence key.</param>
        /// <returns>A list of tuples containing the item, its persister and the key to use during persistence.</returns>
        protected virtual IEnumerable<Tuple<object, ITombstone, string>> GetPersistableItems(List<object> items, string key)
        {
            return items.Select((x, i) =>{
                var persister = x.GetType().GetAttributes<ITombstone>(true).FirstOrDefault();
                return persister != null ? new Tuple<object, ITombstone, string>(x, persister, key + "|" + i) : null;
            }).Where(x => x != null);
        }

        /// <summary>
        /// Gets all the persistable properties of the instance.
        /// </summary>
        /// <param name="instance">The instance to inspect for persistable properties.</param>
        /// <param name="key">The root persistence key.</param>
        /// <returns>A list of tuples containing, the property, its persister and the key to use during persistence.</returns>
        protected virtual IEnumerable<Tuple<PropertyInfo, ITombstone, string>> GetPersistableProperties(object instance, string key)
        {
            return from property in instance.GetType().GetProperties()
                   let attribute = property.GetAttributes<ITombstone>(true).FirstOrDefault()
                   where attribute != null
                   select new Tuple<PropertyInfo, ITombstone, string>(property, attribute, key + "|" + property.Name);
        }

        /// <summary>
        /// Determines the persistence key for the instance.
        /// </summary>
        /// <param name="instance">The instance to determine the key for.</param>
        /// <param name="rootKey">The root key.</param>
        /// <returns>The key.</returns>
        protected virtual string GetStateKey(object instance, string rootKey)
        {
            var key = string.IsNullOrEmpty(rootKey) ? string.Empty : rootKey + "|";
            return key + instance.GetType().Name;
        }
    }
}