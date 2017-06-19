using System.Windows.Controls;
using Caliburn.Micro.Tests.NET45.MockClasses;

namespace Caliburn.Micro.Tests.NET45 {
    using Xunit;
    using System;

    public class ViewCreationTests {

        [Fact]
        public void DefaultViewCreationConfigShouldCallGetInstance() {
            // Arrange
            bool iocIscalled = false;
            IoC.GetInstance = (type, s) => {
                iocIscalled = true;
                return null;
            };
            ViewLocator.ConfigureViewCreation(new ViewCreationConfiguration());
            
            // Act
            ViewLocator.GetOrCreateViewType(typeof(MyView));

            // Assert
            Assert.True(iocIscalled);
        }

        [Fact]
        public void DefaultViewCreationConfigShouldNotCallGetAllInstances()
        {
            // Arrange
            bool iocIscalled = false;
            IoC.GetAllInstances = (type) => {
                iocIscalled = true;
                return null;
            };
            ViewLocator.ConfigureViewCreation(new ViewCreationConfiguration());

            // Act
            ViewLocator.GetOrCreateViewType(typeof(MyView));

            // Assert
            Assert.False(iocIscalled);
        }

        [Fact]
        public void DefaultViewCreationConfigWithConstructorParameterThrows() {
            // Arrange
            IoC.GetInstance = (type, s) => null;
            ViewLocator.ConfigureViewCreation(new ViewCreationConfiguration());

            // Assert
            Assert.Throws<ViewCantBeCreatedException>(() => ViewLocator.GetOrCreateViewType(typeof(MyViewWithDependencies)));
        }

        [Fact]
        public void DefaultViewCreationConfigWithConstructorParameterIsCreatedByIocSucceeds()
        {
            // Arrange
            IoC.GetInstance = (type, s) => new MyViewWithDependencies(new MyViewModel());
            ViewLocator.ConfigureViewCreation(new ViewCreationConfiguration());

            object view = null;

            // Assert
            Assert.DoesNotThrow(() => view = ViewLocator.GetOrCreateViewType(typeof(MyViewWithDependencies)));
            Assert.NotNull(view);
            Assert.IsType<MyViewWithDependencies>(view);
        }

        [Fact]
        public void DefaultViewCreationConfigIoCThrowsReachesUser()
        {
            // Arrange
            IoC.GetInstance = (type, s) => throw new InvalidOperationException($"{type.FullName} could not be created!");
            ViewLocator.ConfigureViewCreation(new ViewCreationConfiguration());

            // Assert
            Assert.Throws<InvalidOperationException>(() => ViewLocator.GetOrCreateViewType(typeof(MyViewWithDependencies)));
        }

        [Fact]
        public void ViewCreationConfigShowEmptyWithConstructorParameterReturnsEmptyView()
        {
            // Arrange
            IoC.GetInstance = (type, s) => null;
            ViewLocator.ConfigureViewCreation(new ViewCreationConfiguration { ShowEmptyViewWhenCreationFailed = true});

            object view = null;

            // Assert
            Assert.DoesNotThrow(() => view = ViewLocator.GetOrCreateViewType(typeof(MyViewWithDependencies)));
            Assert.NotNull(view);
            Assert.IsType<TextBlock>(view);
        }

        [Fact]
        public void V3StyleViewConfigurationShowsEmptyViewByDefault() {
            // Arrange
            var config = new ViewCreationConfiguration(ViewCreationBehavior.UseV3StyleLocation);

            // Assert
            Assert.True(config.ShowEmptyViewWhenCreationFailed);
        }

        [Fact]
        public void V3StyleConfigCallsGetAllInstances() {
            // Arrange
            bool iocCalled = false;
            IoC.GetAllInstances = type => {
                iocCalled = true;
                return null;
            };

            ViewLocator.ConfigureViewCreation(new ViewCreationConfiguration(ViewCreationBehavior.UseV3StyleLocation));

            // Act
            ViewLocator.GetOrCreateViewType(typeof(MyView));

            // Assert
            Assert.True(iocCalled);
        }

        [Fact]
        public void V3StyleConfigDontCallsGetInstance()
        {
            // Arrange
            bool iocCalled = false;
            IoC.GetInstance = (type,s) => {
                iocCalled = true;
                return null;
            };

            ViewLocator.ConfigureViewCreation(new ViewCreationConfiguration(ViewCreationBehavior.UseV3StyleLocation));

            // Act
            ViewLocator.GetOrCreateViewType(typeof(MyView));

            // Assert
            Assert.False(iocCalled);
        }

        [Fact]
        public void DontUseDiContainerConfigDoesNotCallIoc() {
            // Arrange
            bool iocCalled = false;
            IoC.GetInstance = (type, s) => {
                iocCalled = true;
                return null;
            };
            IoC.GetAllInstances= (type) => {
                iocCalled = true;
                return null;
            };
            ViewLocator.ConfigureViewCreation(new ViewCreationConfiguration(ViewCreationBehavior.DontUseDi));

            // Act
            ViewLocator.GetOrCreateViewType(typeof(MyView));

            // Assert
            Assert.False(iocCalled);
        }

        [Fact]
        public void ViewCreationFailsWithExpectedMessageNoDefaultConstructor()
        {
            // Arrange
            ViewLocator.ConfigureViewCreation(new ViewCreationConfiguration(ViewCreationBehavior.DontUseDi));

            // Assert
            var exception = Assert.Throws<ViewCantBeCreatedException>(() => 
                        ViewLocator.GetOrCreateViewType(typeof(MyViewWithDependencies)));
            Assert.Contains("because the type has no default public constructor.", exception.Message);
        }

        [Fact]
        public void ViewCreationFailsWithExpectedMessageNoUIElement()
        {
            // Arrange
            ViewLocator.ConfigureViewCreation(new ViewCreationConfiguration(ViewCreationBehavior.DontUseDi));

            // Assert
            var exception = Assert.Throws<ViewCantBeCreatedException>(() =>
                ViewLocator.GetOrCreateViewType(typeof(MyViewModel)));
            Assert.Contains("because the type is abstract or not of type `UIElement`", exception.Message);
        }
    }
}