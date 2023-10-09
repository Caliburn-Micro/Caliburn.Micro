using System;

using Xunit;

namespace Caliburn.Micro.Platform.Tests
{
    public class AssemblyCacheTests
    {
        [Fact]
        public void AddingTheSameAssemblyMoreThanOneShouldNotThrow()
        {
            AssemblySourceCache.Install();

            System.Reflection.Assembly testAssembly = typeof(AssemblyCacheTests).Assembly;
            AssemblySource.Instance.Add(testAssembly);

            /* Re-add the same assembly */
            Exception exception = Record.Exception(() => AssemblySource.Instance.Add(testAssembly));
            Assert.Null(exception);
        }

        [Fact]
        public void ResettingTheCacheWithMoreThanOneAssemblyShouldNotThrow()
        {
            AssemblySourceCache.Install();

            System.Reflection.Assembly testAssembly = typeof(AssemblyCacheTests).Assembly;

            AssemblySource.Instance.AddRange(new[] { testAssembly, testAssembly });

            /* Refresh clears and re-creates the cache */
            Exception exception = Record.Exception(() => AssemblySource.Instance.Refresh());
            Assert.Null(exception);
        }
    }
}
