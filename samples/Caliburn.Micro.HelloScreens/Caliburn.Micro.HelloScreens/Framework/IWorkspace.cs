namespace Caliburn.Micro.HelloScreens.Framework {
    public interface IWorkspace {
        string Icon { get; }
        string IconName { get; }
        string Status { get; }

        void Show();
    }
}