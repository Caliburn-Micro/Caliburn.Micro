using Avalonia;
using Avalonia.Controls;

namespace Caliburn.Micro.Avalonia.Tests
{
    public class BindingScopeFindName
    {

        [UIFact]
        public void A_given_match_is_always_the_first_instance_found()
        {
            var elements = new List<Control>
            {
                new Control
                {
                    Name = "Foo"
                },
                new Control
                {
                    Name = "Foo"
                }
            };

            var found = BindingScope.FindName(elements, "Foo");
            Assert.NotSame(elements.Last(), found);
        }

        [UIFact]
        public void A_given_name_is_found_regardless_of_case_sensitivity()
        {
            var elements = new List<Control>
            {
                new Control
                {
                    Name = "FOO"
                }
            };

            var found = BindingScope.FindName(elements, "foo");
            Assert.NotNull(found);
        }

        [UIFact]
        public void A_given_name_is_matched_correctly()
        {
            var elements = new List<Control>
            {
                new Control
                {
                    Name = "Bar"
                },
                new Control
                {
                    Name = "Foo"
                }
            };

            var found = BindingScope.FindName(elements, "Foo");
            Assert.NotNull(found);
        }

    }

    public class BindingScope_FindScopeNamingRoute
    {

        [UIFact]
        public void A_given_Pages_Content_is_ScopeRoute_if_it_is_a_dependency_object()
        {
            var page = new UserControl
            {
                Content = new Control()
            };
            var route = BindingScope.FindScopeNamingRoute(page);

            Assert.Same(page.Content, route.Root);
        }

        [UIFact]
        public void A_given_UserControl_is_ScopeRoute()
        {
            var userControl = new UserControl();
            var route = BindingScope.FindScopeNamingRoute(userControl);

            Assert.Same(userControl, route.Root);
        }

        [UIFact]
        public void Any_DependencyObject_is_ScopeRoot_if_IsScopeRoot_is_true()
        {
            var dependencyObject = new AvaloniaObject();
            dependencyObject.SetValue(View.IsScopeRootProperty, true);
            var route = BindingScope.FindScopeNamingRoute(dependencyObject);

            Assert.Same(dependencyObject, route.Root);
        }

    }
}
