namespace Caliburn.Micro.BubblingAction
{
    using System;
    using System.ComponentModel.Composition;

    [Export(typeof(IShell))]
    public class ShellViewModel : IShell
    {
        public BindableCollection<Model> Items { get; private set; }

        public ShellViewModel()
        {
            Items = new BindableCollection<Model>{
                new Model { Id = Guid.NewGuid() },
                new Model { Id = Guid.NewGuid() },
                new Model { Id = Guid.NewGuid() },
                new Model { Id = Guid.NewGuid() }
            };
        }

        public void Add()
        {
            Items.Add(new Model { Id = Guid.NewGuid() });
        }

        public void Remove(Model child)
        {
            Items.Remove(child);
        }
    }
}