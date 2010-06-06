namespace Caliburn.Micro
{
    public interface IHandler<in T>
    {
        void Handle(T message);
    }
}