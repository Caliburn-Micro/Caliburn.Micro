using Microsoft.UI.Xaml;
using Xunit;

namespace Caliburn.Micro.Uno.Tests
{
    public class ScopeNamingRouteTests
    {
        [Fact]
        public void CannotAddDisjointHops()
        {
            var route = new BindingScope.ScopeNamingRoute();
            var d1 = new DependencyObject();
            var d2 = new DependencyObject();
            var d3 = new DependencyObject();
            var d4 = new DependencyObject();

            route.AddHop(d1, d2);
            Assert.Throws<ArgumentException>(() => route.AddHop(d3, d4));
        }

        [Fact]
        public void CorrectlyGetsAddedHop()
        {
            var route = new BindingScope.ScopeNamingRoute();
            var d1 = new DependencyObject();
            var d2 = new DependencyObject();
            route.AddHop(d1, d2);
            var result = route.TryGetHop(d1, out var target);
            Assert.True(result);
            Assert.Same(d2, target);
        }

        [Fact]
        public void GetsAllHopsAdded()
        {
            var route = new BindingScope.ScopeNamingRoute();
            var d1 = new DependencyObject();
            var d2 = new DependencyObject();
            var d3 = new DependencyObject();
            var d4 = new DependencyObject();

            route.AddHop(d1, d2);
            route.AddHop(d2, d3);
            route.AddHop(d3, d4);

            var all = new List<DependencyObject> { d2, d3, d4 };
            var source = d1;

            while (route.TryGetHop(source, out var target))
            {
                all.Remove(target);
                source = target;
            }

            Assert.Empty(all);
        }

        [Fact]
        public void PreventsRoutingCycles()
        {
            var route = new BindingScope.ScopeNamingRoute();
            var d1 = new DependencyObject();
            var d2 = new DependencyObject();
            var d3 = new DependencyObject();
            var d4 = new DependencyObject();

            route.AddHop(d1, d2);
            route.AddHop(d2, d3);
            route.AddHop(d3, d4);
            Assert.Throws<ArgumentException>(() => route.AddHop(d4, d1));
        }

        [Fact]
        public void RootCanBeSetIfNoHops()
        {
            var route = new BindingScope.ScopeNamingRoute();
            var root = new DependencyObject();
            route.Root = root;
            Assert.Same(root, route.Root);
        }

        [Fact]
        public void RootMustNotBeAHopTarget()
        {
            var route = new BindingScope.ScopeNamingRoute();
            var d1 = new DependencyObject();
            var d2 = new DependencyObject();
            var d3 = new DependencyObject();

            route.AddHop(d1, d2);
            route.AddHop(d2, d3);
            Assert.Throws<ArgumentException>(() => route.Root = d2);
        }
    }
}
