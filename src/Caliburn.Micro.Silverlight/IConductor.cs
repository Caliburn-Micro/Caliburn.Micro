namespace Caliburn.Micro
{
    public interface IConductor
    {
        object ActiveItem { get; set; }

        void ActivateItem(object item);
        void CloseItem(object item);
    }
}