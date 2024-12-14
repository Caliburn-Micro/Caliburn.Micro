namespace Caliburn.Micro.Avalonia.Tests
{
    public class ViewLocatorTests
    {
        [Fact]
        public void ConfigureTypeMappingsShouldThrowWhenDefaultSubNamespaceForViewModelsIsEmpty()
        {
            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = "not empty",
                DefaultSubNamespaceForViewModels = string.Empty,
                NameFormat = "not{0} {1}Empty"
            };

            Assert.Throws<ArgumentException>(() => ViewLocator.ConfigureTypeMappings(config));
        }

        [Fact]
        public void ConfigureTypeMappingsShouldThrowWhenDefaultSubNamespaceForViewModelsIsNull()
        {
            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = "not null",
                DefaultSubNamespaceForViewModels = null,
                NameFormat = "{1}not {0}null"
            };

            Assert.Throws<ArgumentException>(() => ViewLocator.ConfigureTypeMappings(config));
        }

        [Fact]
        public void ConfigureTypeMappingsShouldThrowWhenDefaultSubNamespaceForViewsIsEmpty()
        {
            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = string.Empty,
                DefaultSubNamespaceForViewModels = "not Empty",
                NameFormat = "{0}not Empty{1}"
            };

            Assert.Throws<ArgumentException>(() => ViewLocator.ConfigureTypeMappings(config));
        }

        [Fact]
        public void ConfigureTypeMappingsShouldThrowWhenDefaultSubNamespaceForViewsIsNull()
        {
            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = null,
                DefaultSubNamespaceForViewModels = "not null",
                NameFormat = "{0}{1}not null"
            };

            Assert.Throws<ArgumentException>(() => ViewLocator.ConfigureTypeMappings(config));
        }

        [Fact]
        public void ConfigureTypeMappingsShouldThrowWhenNameFormatIsEmpty()
        {
            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = "not Empty",
                DefaultSubNamespaceForViewModels = "not Empty",
                NameFormat = string.Empty
            };

            Assert.Throws<ArgumentException>(() => ViewLocator.ConfigureTypeMappings(config));
        }

        [Fact]
        public void ConfigureTypeMappingsShouldThrowWhenNameFormatIsNull()
        {
            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = "not null",
                DefaultSubNamespaceForViewModels = "not null",
                NameFormat = null
            };

            Assert.Throws<ArgumentException>(() => ViewLocator.ConfigureTypeMappings(config));
        }

        [Fact]
        public void ConfigureTypeMappingsWithDefaultValuesShouldNotThrow()
        {
            var typeMappingConfiguration = new TypeMappingConfiguration();

            ViewLocator.ConfigureTypeMappings(typeMappingConfiguration);
        }
    }
}
