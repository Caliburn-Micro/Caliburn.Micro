namespace Caliburn.Micro
{
    public class ViewCreationConfiguration
    {

        /// <summary>
        /// Creates a default ViewCreationConfiguration
        /// This uses a IoC.GetInstance() to create a view and throws an exception if creation fails
        /// </summary>
        public ViewCreationConfiguration() : this(ViewCreationBehavior.UseDIContainer)
        {
        }

        /// <summary>
        /// Creates a view configuration given a certain <see cref="ViewCreationBehavior"/> 
        /// When <see cref="ViewCreationBehavior.UseV3StyleLocation"/> is used, <see cref="ShowEmptyViewWhenCreationFailed"/> will default to <see langword="true"/>
        /// </summary>
        /// <param name="viewCreationBehavior">The behavior to create views</param>
        public ViewCreationConfiguration(ViewCreationBehavior viewCreationBehavior)
        {
            this.ViewCreationBehavior = viewCreationBehavior;

            this.ShowEmptyViewWhenCreationFailed = viewCreationBehavior == ViewCreationBehavior.UseV3StyleLocation;
        }

        /// <summary>
        /// Determines how views are created
        /// The default is to make a callback where you can hook a DI container
        /// </summary>
        public ViewCreationBehavior ViewCreationBehavior { get; set; }

        /// <summary>
        /// Determines how CM reacts when creation fails. 
        /// When <see langword="true"/> an empty view is shown with a single text explaining the problem. This is the default when using V3Style viewcreation
        /// When <see langword="false"/> an exception is thrown. This is the default
        /// </summary>
        public bool ShowEmptyViewWhenCreationFailed { get; set; }

    }
}