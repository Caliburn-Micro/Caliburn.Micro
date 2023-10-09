using System.Collections.Generic;

namespace Caliburn.Micro {
    /// <summary>
    /// Class to specify settings for configuring type mappings by the ViewLocator or ViewModelLocator.
    /// </summary>
    public class TypeMappingConfiguration {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeMappingConfiguration"/> class.
        /// </summary>
        public TypeMappingConfiguration() {
            DefaultSubNamespaceForViews = "Views";
            DefaultSubNamespaceForViewModels = "ViewModels";
            UseNameSuffixesInMappings = true;
            NameFormat = @"{0}{1}";
            IncludeViewSuffixInViewModelNames = true;
            ViewSuffixList = new List<string>(new[] { "View", "Page" });
            ViewModelSuffix = "ViewModel";
        }

        /// <summary>
        /// Gets or sets the default subnamespace for Views. Used for creating default subnamespace mappings. Defaults to "Views".
        /// </summary>
        public string DefaultSubNamespaceForViews { get; set; }

        /// <summary>
        /// Gets or sets the default subnamespace for ViewModels. Used for creating default subnamespace mappings. Defaults to "ViewModels".
        /// </summary>
        public string DefaultSubNamespaceForViewModels { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether flag to indicate whether or not the name of the Type should be transformed when adding a type mapping. Defaults to true.
        /// </summary>
        public bool UseNameSuffixesInMappings { get; set; }

        /// <summary>
        /// Gets or sets the format string used to compose the name of a type from base name and name suffix.
        /// </summary>
        public string NameFormat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether flag to indicate if ViewModel names should include View suffixes (i.e. CustomerPageViewModel vs. CustomerViewModel).
        /// </summary>
        public bool IncludeViewSuffixInViewModelNames { get; set; }

        /// <summary>
        /// Gets or sets list of View suffixes for which default type mappings should be created. Applies only when UseNameSuffixesInMappings = true.
        /// Default values are "View", "Page".
        /// </summary>
        public List<string> ViewSuffixList { get; set; }

        /// <summary>
        /// Gets or sets the name suffix for ViewModels. Applies only when UseNameSuffixesInMappings = true. The default is "ViewModel".
        /// </summary>
        public string ViewModelSuffix { get; set; }
    }
}
