using System.Windows;
using System.Windows.Controls;

using Xunit;

namespace Caliburn.Micro.Platform.Tests {
    public class BindingScope_FindScopeNamingRoute_Tests {
        [WpfFact]
        public void A_given_Pages_Content_is_ScopeRoute_if_it_is_a_dependency_object() {
            var page = new Page {
                Content = new Control(),
            };
            BindingScope.ScopeNamingRoute route = BindingScope.FindScopeNamingRoute(page);

            Assert.Same(page.Content, route.Root);
        }

        [WpfFact]
        public void A_given_UserControl_is_ScopeRoute() {
            var userControl = new UserControl();
            BindingScope.ScopeNamingRoute route = BindingScope.FindScopeNamingRoute(userControl);

            Assert.Same(userControl, route.Root);
        }

        [WpfFact]
        public void Any_DependencyObject_is_ScopeRoot_if_IsScopeRoot_is_true() {
            var dependencyObject = new DependencyObject();
            dependencyObject.SetValue(View.IsScopeRootProperty, true);
            BindingScope.ScopeNamingRoute route = BindingScope.FindScopeNamingRoute(dependencyObject);

            Assert.Same(dependencyObject, route.Root);
        }
    }
}
