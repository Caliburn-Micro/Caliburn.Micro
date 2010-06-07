namespace Caliburn.Micro
{
    using System.Reflection;

    public static class Bootstrapper
    {
        public static void Initialize(params Assembly[] assembliesToInspect)
        {
            Execute.InitializeWithDispatcher();
            AssemblySource.Assemblies.AddRange(assembliesToInspect);
        }
    }
}