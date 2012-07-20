namespace Caliburn.Micro {
    using System.Collections.Generic;

    /// <summary>
    /// Class to specify settings for configuring type mappings by the ViewLocator or ViewModelLocator
    /// </summary>
    public class TypeMappingConfiguration {
        /// <summary>
        /// The default subnamespace for Views. Used for creating default subnamespace mappings. Defaults to "Views".
        /// </summary>
        public string DefaultSubNamespaceForViews = "Views";

        /// <summary>
        /// The default subnamespace for ViewModels. Used for creating default subnamespace mappings. Defaults to "ViewModels".
        /// </summary>
        public string DefaultSubNamespaceForViewModels = "ViewModels";

        /// <summary>
        /// Flag to indicate whether or not the name of the Type should be transformed when adding a type mapping. Defaults to true.
        /// </summary>
        public bool UseNameSuffixesInMappings = true;

        /// <summary>
        /// The format string used to compose the name of a type from base name and name suffix
        /// </summary>
        public string NameFormat = @"{0}{1}";

        /// <summary>
        /// Flag to indicate if ViewModel names should include View suffixes (i.e. CustomerPageViewModel vs. CustomerViewModel)
        /// </summary>
        public bool IncludeViewSuffixInViewModelNames = true;

        /// <summary>
        /// List of View suffixes for which default type mappings should be created. Applies only when UseNameSuffixesInMappings = true.
        /// Default values are "View", "Page"
        /// </summary>
        public List<string> ViewSuffixList = new List<string>(new[] { "View", "Page" });

        /// <summary>
        /// The name suffix for ViewModels. Applies only when UseNameSuffixesInMappings = true. The default is "ViewModel".
        /// </summary>
        public string ViewModelSuffix = "ViewModel";
    }
}