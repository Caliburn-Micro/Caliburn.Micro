namespace Caliburn.Micro
{
    using System.Reflection;

    public static class AssemblySource
    {
        public static readonly IObservableCollection<Assembly> Known = new BindableCollection<Assembly> {
            Assembly.GetExecutingAssembly()
        };
    }
}