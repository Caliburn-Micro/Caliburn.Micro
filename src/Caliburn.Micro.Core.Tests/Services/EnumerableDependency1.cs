namespace Caliburn.Micro.Core.Tests.Services
{
    internal class EnumerableDependency1 : IEnumerableDependency
    {
        public IDependency2 Dependency2 { get; set; }
    }
}

