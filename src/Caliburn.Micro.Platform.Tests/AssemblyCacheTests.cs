using System;
using System.Globalization;
using Xunit;

namespace Caliburn.Micro.Platform.Tests
{
    public class AssemblyCacheTests
    {
        [Fact]
        public void AddingTheSameAssemblyMoreThanOnceShouldNotThrow()
        {
            AssemblySourceCache.Install();

            var testAssembly = typeof(AssemblyCacheTests).Assembly;
            AssemblySource.Instance.Add(testAssembly);

            //Re-add the same assembly
            var exception = Record.Exception(() => AssemblySource.Instance.Add(testAssembly));
            Assert.Null(exception);
        }
    }

    public class TestScreen : Screen
    {
    }
}
