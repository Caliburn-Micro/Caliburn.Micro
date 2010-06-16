namespace Caliburn.Micro
{
    public interface IHandle<in T>
    {
        void Handle(T message);
    }
}