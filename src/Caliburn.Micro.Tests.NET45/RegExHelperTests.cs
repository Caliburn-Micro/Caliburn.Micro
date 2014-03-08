namespace Caliburn.Micro.WPF.Tests {
    using Xunit;

    public class RegExHelperTests {
        [Fact]
        public void GetCaptureGroupReturnesGroupNameAndRegex() {
            var groupName = "group";
            var regex = ".*";

            var result = RegExHelper.GetCaptureGroup(groupName, regex);

            Assert.Equal("(?<group>.*)", result);
        }

        [Fact]
        public void ShouldEscapeDotsInNamespace() {
            var ns = "Caliburn.Micro.WPF.Tests";

            var result = RegExHelper.NamespaceToRegEx(ns);

            Assert.Equal(@"Caliburn\.Micro\.WPF\.Tests", result);
        }

        [Fact]
        public void ShouldInsertRegexWhenNamespaceHasAsterix() {
            var ns = "Caliburn.*.WPF.Tests";
            
            var result = RegExHelper.NamespaceToRegEx(ns);

            Assert.Equal(@"Caliburn\.([A-Za-z_]\w*\.)*WPF\.Tests", result);
        }

        [Fact]
        public void GetNameCaptureGroupShouldUseNameRegEx() {
            var groupName = "group";

            var result = RegExHelper.GetNameCaptureGroup(groupName);

            Assert.Equal(@"(?<group>[A-Za-z_]\w*)", result);
        }

        [Fact]
        public void GetNameCaptureGroupShouldUseNameSpaceRegEx()
        {
            var groupName = "group";

            var result = RegExHelper.GetNamespaceCaptureGroup(groupName);

            Assert.Equal(@"(?<group>([A-Za-z_]\w*\.)*)", result);
        }
    }
}