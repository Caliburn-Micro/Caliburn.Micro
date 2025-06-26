using Xunit;

namespace Caliburn.Micro.Core.Tests
{
    public class SimpleContainerGettingASingleInstance
    {
        [Fact]
        public void AnInstanceIsReturnedForTheTypeSpecifiedIfFound()
        {
            var container = new SimpleContainer();
            container.PerRequest<object>();

            Assert.NotNull(container.GetInstance(typeof(object), null));
        }

        [Fact]
        public void InstancesCanBeFoundByNameOnly()
        {
            var container = new SimpleContainer();
            container.RegisterPerRequest(typeof(object), "AnObject", typeof(object));

            Assert.NotNull(container.GetInstance(null, "AnObject"));
        }

        [Fact]
        public void NullIsReturnedWhenNoInstanceIsFound()
        {
            Assert.Null(new SimpleContainer().GetInstance(typeof(object), null));
        }
    }
}
