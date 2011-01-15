namespace Caliburn.Micro.HelloScreens.Framework {
    public interface IShell : IConductor, IGuardClose {
        IDialogManager Dialogs { get; }
    }
}