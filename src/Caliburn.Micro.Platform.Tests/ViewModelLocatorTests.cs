using System;
using Xunit;

namespace Caliburn.Micro.Platform.Tests
{
    public class ViewModelLocatorTests
    {
        [Fact]
        public void ConfigureTypeMappingsShouldThrowWhenDefaultSubNamespaceForViewModelsIsEmpty()
        {
            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = "not empty",
                DefaultSubNamespaceForViewModels = string.Empty,
                NameFormat = "not Empty{1}{0}"
            };

            Assert.Throws<ArgumentException>(() => ViewModelLocator.ConfigureTypeMappings(config));
        }

        [Fact]
        public void ConfigureTypeMappingsShouldThrowWhenDefaultSubNamespaceForViewModelsIsNull()
        {
            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = "not null",
                DefaultSubNamespaceForViewModels = null,
                NameFormat = "not null{1}{0}"
            };

            Assert.Throws<ArgumentException>(() => ViewModelLocator.ConfigureTypeMappings(config));
        }

        [Fact]
        public void ConfigureTypeMappingsShouldThrowWhenDefaultSubNamespaceForViewsIsEmpty()
        {
            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = string.Empty,
                DefaultSubNamespaceForViewModels = "not Empty",
                NameFormat = "{0}{1}Empty"
            };

            Assert.Throws<ArgumentException>(() => ViewModelLocator.ConfigureTypeMappings(config));
        }

        [Fact]
        public void ConfigureTypeMappingsShouldThrowWhenDefaultSubNamespaceForViewsIsNull()
        {
            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = null,
                DefaultSubNamespaceForViewModels = "not null",
                NameFormat = "{1}not {0}null"
            };

            Assert.Throws<ArgumentException>(() => ViewModelLocator.ConfigureTypeMappings(config));
        }


        [Fact]
        public void COnfigureTypeMappingsWithDefaultValuesShouldNotThrow()
        {
            var typeMappingConfiguration = new TypeMappingConfiguration();

            ViewModelLocator.ConfigureTypeMappings(typeMappingConfiguration);
        }
        [Fact]
        public void MakeInterface_ShouldReturnInterfaceName()
        {
            // Arrange
            string typeName = "MyClass";
            string expectedInterfaceName = "IMyClass";

            // Act
            string result = ViewModelLocator.MakeInterface(typeName);

            // Assert
            Assert.Equal(expectedInterfaceName, result);
        }

        [Fact]
        public void MakeInterface_ShouldHandleEmptyString()
        {
            // Arrange
            string typeName = "";
            string expectedInterfaceName = "I";

            // Act
            string result = ViewModelLocator.MakeInterface(typeName);

            // Assert
            Assert.Equal(expectedInterfaceName, result);
        }

    }
}
