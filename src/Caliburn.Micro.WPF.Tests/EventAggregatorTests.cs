namespace Caliburn.Micro.WPF.Tests
{
    using Xunit;

    public class EventAggregatorTests
    {
        internal class HandlerStub : IHandle<object> 
        {
            public void Handle(object message) 
            {
                
            }
        }

        [Fact]
        public void Calling_HandlerExistsFor_Returns_True_When_Handlers_Present() 
        {
            var handlerStub = new HandlerStub();
            var aggregator = new EventAggregator();

            aggregator.Subscribe(handlerStub);

            Assert.True(aggregator.HandlerExistsFor(typeof (object)));
        }

        [Fact]
        public void Calling_HandlerExistsFor_Returns_False_When_No_Handlers_Present() 
        {
            var aggregator = new EventAggregator();

            Assert.False(aggregator.HandlerExistsFor(typeof (object)));
        }
    }
}
