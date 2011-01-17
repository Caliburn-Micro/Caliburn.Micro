namespace GameLibrary.Framework {
    using System.Collections.Generic;

    public interface IValidator
    {
        IEnumerable<Error> Validate(object instance);
        IEnumerable<Error> Validate(object instance, string propertyName);
    }
}