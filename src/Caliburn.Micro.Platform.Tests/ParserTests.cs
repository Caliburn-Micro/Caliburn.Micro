using System.Globalization;

using Xunit;

namespace Caliburn.Micro.Platform.Tests
{
    public class ParserTests
    {
        [Fact]
        public void CreateParametersWithOddNumbers()
        {
            Parameter evenResult = Parser.CreateParameter(null, "0.1");
            Parameter oddResult = Parser.CreateParameter(null, "-0.1");
            Parameter nanResult = Parser.CreateParameter(null, "-0.1abc");

            Assert.Equal(0.1, double.Parse((string)evenResult.Value, CultureInfo.InvariantCulture));
            Assert.Equal(-0.1, double.Parse((string)oddResult.Value, CultureInfo.InvariantCulture));
            Assert.Null(nanResult.Value);
        }
    }
}
