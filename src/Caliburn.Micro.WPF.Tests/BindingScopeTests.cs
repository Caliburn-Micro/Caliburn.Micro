using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Xunit;

namespace Caliburn.Micro.WPF.Tests
{
    public class BindingScopeTests
    {
        [Fact]
        public void FindNameMatchesElementNameExactly()
        {
            var elements = new List<FrameworkElement>
                {
                    new FrameworkElement { Name = "Bar" },
                    new FrameworkElement { Name = "Foo" },
                    new FrameworkElement { Name = "Baz" },
                    new FrameworkElement { Name = "Qux" },
                    new FrameworkElement { Name = "Foo" },
                };

            var found = BindingScope.FindName(elements, "Foo");
            Assert.NotNull(found);
            Assert.Equal(1, elements.IndexOf(found));
        }

        [Fact]
        public void FindNameMatchesElementNameCaseInsensitive()
        {
            var elements = new List<FrameworkElement>
                {
                    new FrameworkElement { Name = "BAR" },
                    new FrameworkElement { Name = "FOO" },
                    new FrameworkElement { Name = "BAZ" },
                    new FrameworkElement { Name = "QUX" },
                    new FrameworkElement { Name = "FOO" },
                };

            var found = BindingScope.FindName(elements, "foo");
            Assert.NotNull(found);
            Assert.Equal(1, elements.IndexOf(found));
        }

        [Fact]
        public void FindNameMatchesFirstElement()
        {
            var elements = new List<FrameworkElement>
                {
                    new FrameworkElement { Name = "Bar" },
                    new FrameworkElement { Name = "Foo" },
                    new FrameworkElement { Name = "Baz" },
                    new FrameworkElement { Name = "Qux" },
                    new FrameworkElement { Name = "Foo" },
                };

            var found = BindingScope.FindName(elements, "Foo");
            Assert.NotNull(found);
            Assert.NotSame(elements.Last(), found);
        }

        [Fact]
        public void UserControlIsScopeRoot()
        {
            var userControl = new UserControl();
            var route = BindingScope.FindScopeNamingRoute(userControl);
            Assert.Same(userControl, route.Root);
        }

        [Fact]
        public void PageContentIsScopeRootIfDependencyObject()
        {
            var page = new Page { Content = new Control() };

            var route = BindingScope.FindScopeNamingRoute(page);
            Assert.Same(page.Content, route.Root);
        }

        [Fact]
        public void AnyDependencyObjectIsScopeRootIfIsScopeRootPropertyIsTrue()
        {
            var dependencyObject = new DependencyObject();
            dependencyObject.SetValue(View.IsScopeRootProperty, true);

            var route = BindingScope.FindScopeNamingRoute(dependencyObject);
            Assert.Same(dependencyObject, route.Root);
        }

        [Fact(Skip = "Incomplete")]
        public void FindNamedDescendentsCorrectlyInspectsContentControl()
        {

        }

        [Fact(Skip = "Incomplete")]
        public void FindNamedDescendentsCorrectlyInspectsItemContentControl()
        {

        }
    }
}