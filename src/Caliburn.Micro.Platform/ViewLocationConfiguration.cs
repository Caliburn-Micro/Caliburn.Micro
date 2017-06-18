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


    public class ViewCreationConfiguration {

        public ViewCreationConfiguration() {
            this.ViewCreationBehavior = ViewCreationBehavior.UseDIContainer;
        }

        /// <summary>
        /// Determines how views are created
        /// The default is to make a callback where you can hook a DI container
        /// </summary>
        public ViewCreationBehavior ViewCreationBehavior { get; set; }

        /// <summary>
        /// Determines how CM reacts when creation fails. 
        /// When <see langword="true" an empty view is shown with a single text explaining the problem
        /// When <see langword="false" an exception is thrown. This is the default
        /// </summary>
        public bool ShowEmptyViewWhenCreationFailed { get; set; }

    }
}