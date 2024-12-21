namespace Caliburn.Micro.Core.Tests.Services
{
    public class TwoConstructors
    {
        public int Value { get; set; }

        public TwoConstructors()
        {
            Value = 42;
        }

        public TwoConstructors(int value)
        {
            Value = value;
        }
    }

}
