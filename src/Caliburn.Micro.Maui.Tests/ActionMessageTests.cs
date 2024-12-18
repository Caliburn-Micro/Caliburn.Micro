namespace Caliburn.Micro.Maui.Tests
{
    public class ActionMessageTests
    {
        [Fact]
        public void MethodName_ShouldBeSetAndRetrievedCorrectly()
        {
            // Arrange
            var actionMessage = new ActionMessage();
            var expectedMethodName = "TestMethod";

            // Act
            actionMessage.MethodName = expectedMethodName;
            var actualMethodName = actionMessage.MethodName;

            // Assert
            Assert.Equal(expectedMethodName, actualMethodName);
        }

        [Fact]
        public void Handler_ShouldBeSetAndRetrievedCorrectly()
        {
            // Arrange
            var actionMessage = new ActionMessage();
            var expectedHandler = new object();

            // Act
            actionMessage.Handler = expectedHandler;
            var actualHandler = actionMessage.Handler;

            // Assert
            Assert.Equal(expectedHandler, actualHandler);
        }

        [Fact]
        public void Parameters_ShouldBeInitialized()
        {
            // Arrange
            var actionMessage = new ActionMessage();

            // Act
            var parameters = actionMessage.Parameters;

            // Assert
            Assert.NotNull(parameters);
        }

    }
}
