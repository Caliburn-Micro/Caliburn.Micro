namespace Caliburn.Micro
{
    public interface IViewAware
    {
        void AttachView(object view, object context = null);
        object GetView(object context = null);
    }
}