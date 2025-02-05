using System.Collections.Generic;

namespace Caliburn.Micro.Core.Tests.Services
{
    internal class Dependency1 : IDependency1
    {
        public IDependency2 Dependency2 { get; set; }
        public IList<IEnumerableDependency> EnumerableDependencies { get; set; }
    }
}

