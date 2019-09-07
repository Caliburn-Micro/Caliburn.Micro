using Xunit;

namespace Caliburn.Micro.Core.Tests
{
    public class ConductorCollectionAllActiveTests
    {
        [Fact]
        public void ParentItemIsUnsetOnClear()
        {
            var conductor = new Conductor<IScreen>.Collection.AllActive();
            var conducted = new[] { new Screen(), new Screen() };
            conductor.Items.AddRange(conducted);

            conductor.Items.Clear();

            Assert.All(conducted, screen => Assert.NotEqual(conductor, screen.Parent));
        }
    }
}
