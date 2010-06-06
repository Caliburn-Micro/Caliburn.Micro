namespace Caliburn.Micro
{
    using System;

    public interface IActivate
    {
        void Activate();
    }

    public interface IDeactivate
    {
        void Deactivate(bool close);
    }

    public interface IGuardClose
    {
        void CanClose(Action<bool> callback);
    }

    public interface IHaveDisplayName
    {
        string DisplayName { get; set; }
    }

    public interface IScreen : IHaveDisplayName, IActivate, IDeactivate, IGuardClose, INotifyPropertyChangedEx
    {
    }
}