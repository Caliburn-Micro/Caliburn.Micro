using Avalonia;

namespace Caliburn.Micro.Platform.Tests
{
    public class ScopeNamingRouteTests
    {
        [Fact]
        public void CannotAddDisjointHops()
        {
            var route = new BindingScope.ScopeNamingRoute();
            var d1 = new AvaloniaObject();
            var d2 = new AvaloniaObject();
            var d3 = new AvaloniaObject();
            var d4 = new AvaloniaObject();

            // d1 -> d2 and d3 -> d4, but d2 doesn't lead to d3, so d3 -> d4 is rejected
            route.AddHop(d1, d2);
            Assert.Throws<ArgumentException>(() => route.AddHop(d3, d4));
        }

        [Fact]
        public void CorrectlyGetsAddedHop()
        {
            var route = new BindingScope.ScopeNamingRoute();
            var d1 = new AvaloniaObject();
            var d2 = new AvaloniaObject();
            route.AddHop(d1, d2);
            var result = route.TryGetHop(d1, out AvaloniaObject target);
            Assert.True(result);
            Assert.Same(d2, target);
        }

        [Fact]
        public void GetsAllHopsAdded()
        {
            var route = new BindingScope.ScopeNamingRoute();
            var d1 = new AvaloniaObject();
            var d2 = new AvaloniaObject();
            var d3 = new AvaloniaObject();
            var d4 = new AvaloniaObject();

            route.AddHop(d1, d2);
            route.AddHop(d2, d3);
            route.AddHop(d3, d4);

            var all = new List<AvaloniaObject>
            {
                d2,
                d3,
                d4
            };

            var source = d1;

            while (route.TryGetHop(source, out AvaloniaObject target))
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
            var d1 = new AvaloniaObject();
            var d2 = new AvaloniaObject();
            var d3 = new AvaloniaObject();
            var d4 = new AvaloniaObject();

            route.AddHop(d1, d2);
            route.AddHop(d2, d3);
            route.AddHop(d3, d4);
            Assert.Throws<ArgumentException>(() => route.AddHop(d4, d1));
        }

        [Fact]
        public void RootCanBeSetIfNoHops()
        {
            var route = new BindingScope.ScopeNamingRoute();
            var root = new AvaloniaObject();
            route.Root = root;
            Assert.Same(root, route.Root);
        }

        [Fact]
        public void RootMustNotBeAHopTarget()
        {
            var route = new BindingScope.ScopeNamingRoute();
            var d1 = new AvaloniaObject();
            var d2 = new AvaloniaObject();
            var d3 = new AvaloniaObject();

            route.AddHop(d1, d2);
            route.AddHop(d2, d3);
            Assert.Throws<ArgumentException>(() => route.Root = d2);
        }
    }
}
