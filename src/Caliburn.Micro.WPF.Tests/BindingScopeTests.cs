namespace Caliburn.Micro.WPF.Tests {

    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using Xunit;

    public class BindingScope_FindName {
        [Fact]
        public void A_given_name_is_matched_correctly() {
            var elements = new List<FrameworkElement> {
                    new FrameworkElement { Name = "Bar" },
                    new FrameworkElement { Name = "Foo" },
                };

            var found = BindingScope.FindName(elements, "Foo");
            Assert.NotNull(found);
        }

        [Fact]
        public void A_given_name_is_found_regardless_of_case_sensitivity() {
            var elements = new List<FrameworkElement> {
                    new FrameworkElement { Name = "FOO" },
                };

            var found = BindingScope.FindName(elements, "foo");
            Assert.NotNull(found);
        }

        [Fact]
        public void A_given_match_is_always_the_first_instance_found() {
            var elements = new List<FrameworkElement> {
                    new FrameworkElement {Name = "Foo"},
                    new FrameworkElement {Name = "Foo"},
                };

            var found = BindingScope.FindName(elements, "Foo");
            Assert.NotSame(elements.Last(), found);
        }
    }

    public class BindingScope_FindScopeNamingRoute {
        [Fact]
        public void A_given_UserControl_is_ScopeRoute() {
            var userControl = new UserControl();
            var route = BindingScope.FindScopeNamingRoute(userControl);

            Assert.Same(userControl, route.Root);
        }

        [Fact]
        public void A_given_Pages_Content_is_ScopeRoute_if_it_is_a_dependency_object() {
            var page = new Page { Content = new Control() };
            var route = BindingScope.FindScopeNamingRoute(page);

            Assert.Same(page.Content, route.Root);
        }

        [Fact]
        public void Any_DependencyObject_is_ScopeRoot_if_IsScopeRoot_is_true() {
            var dependencyObject = new DependencyObject();
            dependencyObject.SetValue(View.IsScopeRootProperty, true);
            var route = BindingScope.FindScopeNamingRoute(dependencyObject);

            Assert.Same(dependencyObject, route.Root);
        }
    }
}