namespace Caliburn.Micro.Core.Tests.Services
{
    internal class Component : IComponent
    {
        public IDependency1 Dependency1 { get; set; }
        public NonInterfaceDependency NonInterfaceDependency { get; set; }
    }
}

