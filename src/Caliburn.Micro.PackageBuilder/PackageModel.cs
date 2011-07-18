namespace Caliburn.Micro.PackageBuilder {
    using System.Collections.Generic;

    public class PackageModel {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }

        public List<FrameworkAssembly> FrameworkAssemblies { get; set; }
        public List<string> Content { get; set; }

        public PackageModel() {
            FrameworkAssemblies = new List<FrameworkAssembly>();
            Content = new List<string>();
        }
    }
}