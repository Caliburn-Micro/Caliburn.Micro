using Xunit;

namespace Caliburn.Micro.Core.Tests
{
    public class SimpleContainerGettingASingleInstance
    {
        [Fact]
        public void An_instance_is_returned_for_the_type_specified_if_found()
        {
            var container = new SimpleContainer();
            container.PerRequest<object>();

            Assert.NotNull(container.GetInstance(typeof(object), null));
        }

        [Fact]
        public void Instances_can_be_found_by_name_only()
        {
            var container = new SimpleContainer();
            container.RegisterPerRequest(typeof(object), "AnObject", typeof(object));

            Assert.NotNull(container.GetInstance(null, "AnObject"));
        }

        [Fact]
        public void Null_is_returned_when_no_instance_is_found()
        {
            Assert.Null(new SimpleContainer().GetInstance(typeof(object), null));
        }
    }
}
