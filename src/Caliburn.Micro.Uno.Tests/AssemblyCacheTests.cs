using System;
using Xunit;

namespace Caliburn.Micro.Uno.Tests
{
    public class AssemblyCacheTests
    {
        [Fact]
        public void AddingTheSameAssemblyMoreThanOneShouldNotThrow()
        {
            AssemblySourceCache.Install();

            var testAssembly = typeof(AssemblyCacheTests).Assembly;
            AssemblySource.Instance.Add(testAssembly);

            var exception = Record.Exception(() => AssemblySource.Instance.Add(testAssembly));
            Assert.Null(exception);
        }

        [Fact]
        public void ResettingTheCacheWithMoreThanOneAssemblyShouldNotThrow()
        {
            AssemblySourceCache.Install();

            var testAssembly = typeof(AssemblyCacheTests).Assembly;
            AssemblySource.Instance.AddRange(new[] { testAssembly, testAssembly });

            var exception = Record.Exception(() => AssemblySource.Instance.Refresh());
            Assert.Null(exception);
        }
    }

    public class TestScreen : Screen
    {
    }
}
