namespace Caliburn.Micro
{
    public interface IChild<TParent>
    {
        TParent Parent { get; set; }
    }
}