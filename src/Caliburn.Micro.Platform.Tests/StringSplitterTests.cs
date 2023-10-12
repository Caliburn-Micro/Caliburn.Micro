using Xunit;

namespace Caliburn.Micro.Platform.Tests {
    public class StringSplitterTests {
        [Fact]
        public void SplitSimpleString() {
            string[] output = StringSplitter.Split("MyMethodName", ';');

            Assert.Collection(output, o => Assert.Equal("MyMethodName", o));
        }

        [Fact]
        public void SplitSeparatedString() {
            string[] output = StringSplitter.Split("First;Second", ';');

            Assert.Collection(
                output,
                o => Assert.Equal("First", o),
                o => Assert.Equal("Second", o));
        }

        [Fact]
        public void TrimsSplitSimpleString() {
            string[] output = StringSplitter.Split("MyMethodName  ", ';');

            Assert.Collection(output, o => Assert.Equal("MyMethodName", o));
        }

        [Fact]
        public void RemovesEmptySplitsSeparatedString() {
            string[] output = StringSplitter.Split("First;Second;", ';');

            Assert.Collection(
                output,
                o => Assert.Equal("First", o),
                o => Assert.Equal("Second", o));
        }

        [Fact]
        public void HandlesNewLinesInSeparatedString() {
            string[] output = StringSplitter.Split(
                @"
                First;
                Second;
            ",
                ';');

            Assert.Collection(
                output,
                o => Assert.Equal("First", o),
                o => Assert.Equal("Second", o));
        }
    }
}
