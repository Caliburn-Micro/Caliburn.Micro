using Xunit;

namespace Caliburn.Micro.Platform.Tests
{
    public class StringSplitterTests
    {
        [Fact]
        public void SplitSimpleString()
        {
            var output = StringSplitter.Split("MyMethodName", ';');

            Assert.Collection(output, o => Assert.Equal("MyMethodName", o));
        }

        [Fact]
        public void SplitSeparatedString()
        {
            var output = StringSplitter.Split("First;Second", ';');

            Assert.Collection(output, 
                o => Assert.Equal("First", o),
                o => Assert.Equal("Second", o));
        }

        [Fact]
        public void TrimsSplitSimpleString()
        {
            var output = StringSplitter.Split("MyMethodName  ", ';');

            Assert.Collection(output, o => Assert.Equal("MyMethodName", o));
        }

        [Fact]
        public void RemovesEmptySplitsSeparatedString()
        {
            var output = StringSplitter.Split("First;Second;", ';');

            Assert.Collection(output,
                o => Assert.Equal("First", o),
                o => Assert.Equal("Second", o));
        }

        [Fact]
        public void HandlesNewLinesInSeparatedString()
        {
            var output = StringSplitter.Split(@"
                First;
                Second;
            ", ';');

            Assert.Collection(output,
                o => Assert.Equal("First", o),
                o => Assert.Equal("Second", o));
        }
    }
}
