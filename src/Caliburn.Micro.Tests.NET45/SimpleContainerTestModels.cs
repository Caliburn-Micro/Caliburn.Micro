using System.Collections.Generic;

namespace Caliburn.Micro.Tests.NET45
{
    public interface IComponent { }
    public interface IDependency1 { }
    public interface IDependency2 { }
    public interface IEnumerableDependency { }

    public class Component : IComponent {
        public IDependency1 Dependency1 { get; set; }
        public NonInterfaceDependency NonInterfaceDependency { get; set; }
    }

    public class Dependency1 : IDependency1 {
        public IDependency2 Dependency2 { get; set; }
        public IList<IEnumerableDependency> EnumerableDependencies { get; set; }
    }

    public class Dependency2 : IDependency2 { }

    public class EnumerableDependency1 : IEnumerableDependency {
        public IDependency2 Dependency2 { get; set; }
    }

    public class EnumerableDependency2 : IEnumerableDependency { }

    public class NonInterfaceDependency { }

    public class SecondDependency1 : Dependency1 { }
}