namespace Caliburn.Micro.Core.Tests.Services
{
    public class SingleIntConstructor
    {
        public int Value { get; private set; }

        public SingleIntConstructor(int x)
        {
            this.Value = x;
        }
    }

}
