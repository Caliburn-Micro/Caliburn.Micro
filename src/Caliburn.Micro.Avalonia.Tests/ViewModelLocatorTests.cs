namespace Caliburn.Micro.Avalonia.Tests
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
                NameFormat = "{0}{1}not Empty"
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
                NameFormat = "{1}{0}not null"
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
                NameFormat = "{0}{1}not Empty"
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
                NameFormat = "{0}{1}not null"
            };

            Assert.Throws<ArgumentException>(() => ViewModelLocator.ConfigureTypeMappings(config));
        }


        [Fact]
        public void COnfigureTypeMappingsWithDefaultValuesShouldNotThrow()
        {
            var typeMappingConfiguration = new TypeMappingConfiguration();

            ViewModelLocator.ConfigureTypeMappings(typeMappingConfiguration);
        }
    }
}
