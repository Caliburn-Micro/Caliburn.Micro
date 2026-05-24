using Moq;

namespace Caliburn.Micro.Maui.Tests
{
    public class FormsPlatformProviderTests
    {
        private readonly Mock<IPlatformProvider> mockPlatformProvider;
        private readonly FormsPlatformProvider formsPlatformProvider;

        public FormsPlatformProviderTests()
        {
            mockPlatformProvider = new Mock<IPlatformProvider>();
            formsPlatformProvider = new FormsPlatformProvider(mockPlatformProvider.Object);
        }

        [Fact]
        public void InDesignMode_ShouldReturnPlatformProviderInDesignMode()
        {
            // Arrange
            mockPlatformProvider.Setup(p => p.InDesignMode).Returns(true);

            // Act
            var result = formsPlatformProvider.InDesignMode;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void PropertyChangeNotificationsOnUIThread_ShouldReturnPlatformProviderPropertyChangeNotificationsOnUIThread()
        {
            // Arrange
            mockPlatformProvider.Setup(p => p.PropertyChangeNotificationsOnUIThread).Returns(true);

            // Act
            var result = formsPlatformProvider.PropertyChangeNotificationsOnUIThread;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void BeginOnUIThread_ShouldCallPlatformProviderBeginOnUIThread()
        {
            // Arrange
            System.Action action = () => { };

            // Act
            formsPlatformProvider.BeginOnUIThread(action);

            // Assert
            mockPlatformProvider.Verify(p => p.BeginOnUIThread(action), Times.Once);
        }

        [Fact]
        public async Task OnUIThreadAsync_ShouldCallPlatformProviderOnUIThreadAsync()
        {
            // Arrange
            Func<Task> action = async () => await Task.CompletedTask;

            // Act
            await formsPlatformProvider.OnUIThreadAsync(action);

            // Assert
            mockPlatformProvider.Verify(p => p.OnUIThreadAsync(action), Times.Once);
        }

        [Fact]
        public void OnUIThread_ShouldCallPlatformProviderOnUIThread()
        {
            // Arrange
            System.Action action = () => { };

            // Act
            formsPlatformProvider.OnUIThread(action);

            // Assert
            mockPlatformProvider.Verify(p => p.OnUIThread(action), Times.Once);
        }

        [Fact]
        public void ExecuteOnFirstLoad_ShouldCallPlatformProviderExecuteOnFirstLoad_WhenViewIsNotPage()
        {
            // Arrange
            var view = new object();
            Action<object> handler = _ => { };

            // Act
            formsPlatformProvider.ExecuteOnFirstLoad(view, handler);

            // Assert
            mockPlatformProvider.Verify(p => p.ExecuteOnFirstLoad(view, handler), Times.Once);
        }


        [Fact]
        public void ExecuteOnLayoutUpdated_ShouldCallPlatformProviderExecuteOnLayoutUpdated()
        {
            // Arrange
            var view = new object();
            Action<object> handler = _ => { };

            // Act
            formsPlatformProvider.ExecuteOnLayoutUpdated(view, handler);

            // Assert
            mockPlatformProvider.Verify(p => p.ExecuteOnLayoutUpdated(view, handler), Times.Once);
        }

        [Fact]
        public void GetViewCloseAction_ShouldCallPlatformProviderGetViewCloseAction()
        {
            // Arrange
            var viewModel = new object();
            var views = new List<object>();
            bool? dialogResult = true;

            // Act
            formsPlatformProvider.GetViewCloseAction(viewModel, views, dialogResult);

            // Assert
            mockPlatformProvider.Verify(p => p.GetViewCloseAction(viewModel, views, dialogResult), Times.Once);
        }
    }

}
