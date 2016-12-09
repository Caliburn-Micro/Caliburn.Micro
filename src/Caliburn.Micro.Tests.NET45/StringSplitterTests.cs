namespace Caliburn.Micro.WPF.Tests
{

    using Xunit;

    public class StringSplitterTests
    {

        [Fact]
        public void ShouldSplitAsNormalWhenStringContainsNoBrackets()
        {
            var message = "message string for testing";
            var separator = ' ';

            var strings = StringSplitter.Split(message, separator);

            Assert.Equal(4, strings.Length);
        }

        [Fact]
        public void ShouldNotSplitValuesWithinBrackets()
        {
            var message = "[message string] for testing";
            var separator = ' ';

            var strings = StringSplitter.Split(message, separator);

            Assert.Equal(3, strings.Length);
        }

        [Fact]
        public void ShouldSplitParametersOnComma()
        {
            var message = "message, string, for, testing";

            var strings = StringSplitter.SplitParameters(message);

            Assert.Equal(4, strings.Length);
        }

        [Fact]
        public void ShouldNotSplitStringsInsideQuotes() {
            var message = "message, \"string, for\", testing";

            var strings = StringSplitter.SplitParameters(message);

            Assert.Equal(3, strings.Length);
        }

        [Fact]
        public void ShouldNotSplitStringsInsideCurlyBraces()
        {
            var message = "message, {string, for}, testing";

            var strings = StringSplitter.SplitParameters(message);

            Assert.Equal(3, strings.Length);
        }

        [Fact]
        public void ShouldNotSplitStringsInsideSquareBrackets()
        {
            var message = "message, [string, for], testing";

            var strings = StringSplitter.SplitParameters(message);

            Assert.Equal(3, strings.Length);
        }

        [Fact]
        public void ShouldNotSplitStringsInsideParenthesis()
        {
            var message = "message, (string, for), testing";

            var strings = StringSplitter.SplitParameters(message);

            Assert.Equal(3, strings.Length);
        }
    }
}