namespace Caliburn.Micro
{
    using System;

    public interface IActivate
    {
        void Activate();
        event EventHandler<ActivationEventArgs> Activated;
    }

    public interface IDeactivate
    {
        void Deactivate(bool close);
        event EventHandler<DeactivationEventArgs> Deactivated;
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

    public class ActivationEventArgs : EventArgs
    {
        public bool WasInitialized;
    }

    public class DeactivationEventArgs : EventArgs
    {
        public bool WasClosed;
    }
}