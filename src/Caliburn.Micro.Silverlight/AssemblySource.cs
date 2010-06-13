namespace Caliburn.Micro
{
    using System.Reflection;

    public static class AssemblySource
    {
        public static readonly IObservableCollection<Assembly> Instance = new BindableCollection<Assembly>();
    }
}