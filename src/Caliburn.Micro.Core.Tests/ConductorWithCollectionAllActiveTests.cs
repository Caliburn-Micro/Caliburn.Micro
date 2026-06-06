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

            Assert.All(conducted, screen => Assert.Null(screen.Parent));
        }

        [Fact]
        public void ParentItemIsUnsetOnRemoveRange()
        {
            var conductor = new Conductor<IScreen>.Collection.AllActive();
            var conducted = new[] { new Screen(), new Screen() };
            conductor.Items.AddRange(conducted);

            conductor.Items.RemoveRange(conducted);

            Assert.All(conducted, screen => Assert.Null(screen.Parent));
        }
    }
}
