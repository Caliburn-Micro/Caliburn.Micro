namespace Caliburn.Micro.Maui.Tests
{
    using Microsoft.Maui.Controls;
    using Moq;
    using Xunit;


    public class ParameterTests
    {
        [Fact]
        public void ValueProperty_Should_SetAndGetValue()
        {
            // Arrange
            var parameter = new Parameter();
            var expectedValue = "TestValue";

            // Act
            parameter.Value = expectedValue;
            var actualValue = parameter.Value;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Attach_Should_SetAssociatedObject()
        {
            // Arrange
            var parameter = new Parameter();
            var bindableObject = new CheckBox();

            // Act
            ((IAttachedObject)parameter).Attach(bindableObject);

            // Assert
            Assert.Equal(bindableObject, ((IAttachedObject)parameter).AssociatedObject);
        }

        [Fact]
        public void Detach_Should_ClearAssociatedObject()
        {
            // Arrange
            var parameter = new Parameter();
            var bindableObject = new TextCell();
            ((IAttachedObject)parameter).Attach(bindableObject);

            // Act
            ((IAttachedObject)parameter).Detach();

            // Assert
            Assert.Null(((IAttachedObject)parameter).AssociatedObject);
        }

        [Fact]
        public void OnValueChanged_Should_UpdateAvailability()
        {
            // Arrange
            var parameter = new Parameter();
            var mockActionMessage = new Mock<ActionMessage>();
            mockActionMessage.Setup(x => x.UpdateAvailability());
            var actionMessage = mockActionMessage.Object;
            parameter.MakeAwareOf(actionMessage);

            // Act
            Parameter.OnValueChanged(parameter, new DependencyPropertyChangedEventArgs(null, null, Parameter.ValueProperty));

            // Assert
            mockActionMessage.Verify(v => v.UpdateAvailability());
        }


        [Fact]
        public void ExecuteOnFirstLoad_ShouldCallBaseMethod_WhenViewIsNotPage()
        {
            // Arrange
            var mockPlatformProvider = new Mock<IPlatformProvider>();
            var formsPlatformProvider = new FormsPlatformProvider(mockPlatformProvider.Object);
            var mockView = new Mock<object>();
            Action<object> handler = (view) => { };

            // Act
            formsPlatformProvider.ExecuteOnFirstLoad(mockView.Object, handler);

            // Assert
            mockPlatformProvider.Verify(p => p.ExecuteOnFirstLoad(mockView.Object, handler), Times.Once);
        }
    }


}
