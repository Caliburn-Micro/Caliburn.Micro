using System;
using System.Globalization;
using Xunit;

namespace Caliburn.Micro.Platform.Tests
{
    public class AssemblyCacheTests
    {
        [Fact]
        public void AddingTheSameAssemblyMoreThanOneShouldNotThrow()
        {
            AssemblySourceCache.Install();

            var testAssembly = typeof(AssemblyCacheTests).Assembly;
            AssemblySource.Instance.Add(testAssembly);

            //Re-add the same assembly
            var exception = Record.Exception(() => AssemblySource.Instance.Add(testAssembly));
            Assert.Null(exception);
        }

        [Fact]
        public void ResettingTheCacheWithMoreThanOneAssemblyShouldNotThrow()
        {
            AssemblySourceCache.Install();

            var testAssembly = typeof(AssemblyCacheTests).Assembly;

            AssemblySource.Instance.AddRange(new[] { testAssembly, testAssembly });

            //Refresh clears and re-creates the cache
            var exception = Record.Exception(() => AssemblySource.Instance.Refresh());
            Assert.Null(exception);
        }
    }

    public class TestScreen : Screen
    {
    }
}
