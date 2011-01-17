namespace GameLibrary.Framework {
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using Caliburn.Micro;

    [Export(typeof(IValidator))]
    public class Validator : IValidator {
        public IEnumerable<Error> Validate(object instance) {
            return from property in instance.GetType().GetProperties()
                   from error in GetValidationErrors(instance, property)
                   select error;
        }

        public IEnumerable<Error> Validate(object instance, string propertyName) {
            var property = instance.GetType().GetProperty(propertyName);
            return GetValidationErrors(instance, property);
        }

        IEnumerable<Error> GetValidationErrors(object instance, PropertyInfo property) {
            var context = new ValidationContext(instance, null, null);
            var validators = from attribute in property.GetAttributes<ValidationAttribute>(true)
                             where attribute.GetValidationResult(property.GetValue(instance, null), context) != ValidationResult.Success
                             select new Error(
                                 instance,
                                 property.Name,
                                 attribute.FormatErrorMessage(property.Name)
                                 );

            return validators.OfType<Error>();
        }
    }
}