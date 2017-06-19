namespace Caliburn.Micro {

    public enum ViewCreationBehavior {
        /// <summary>
        /// This is the default location type.
        /// A callback is done to the C.M. root app instance.GetInstance(), when null is returned
        /// C.M. tries to create the view using <see cref="System.Activator"/>.
        /// </summary>
        UseDIContainer,
        /// <summary>
        /// A callback is done to the C.M. root app instance.GetAllInstances(), when null is returned
        /// C.M. tries to create the view using <see cref="System.Activator"/>.
        /// </summary>
        UseV3StyleLocation,
        /// <summary>
        /// The CM root app instance isn't called. CM just creates the view using <see cref="System.Activator"/>.
        /// </summary>
        DontUseDi,
    }
}