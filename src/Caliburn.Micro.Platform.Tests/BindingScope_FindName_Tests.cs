using System.Collections.Generic;
using System.Linq;
using System.Windows;

using Xunit;

namespace Caliburn.Micro.Platform.Tests
{
    public class BindingScope_FindName_Tests
    {
        [WpfFact]
        public void A_given_match_is_always_the_first_instance_found()
        {
            var elements = new List<FrameworkElement>
            {
                new FrameworkElement
                {
                    Name = "Foo",
                },
                new FrameworkElement
                {
                    Name = "Foo",
                },
            };

            FrameworkElement found = BindingScope.FindName(elements, "Foo");
            Assert.NotSame(elements.Last(), found);
        }

        [WpfFact]
        public void A_given_name_is_found_regardless_of_case_sensitivity()
        {
            var elements = new List<FrameworkElement>
            {
                new FrameworkElement
                {
                    Name = "FOO",
                },
            };

            FrameworkElement found = BindingScope.FindName(elements, "foo");
            Assert.NotNull(found);
        }

        [WpfFact]
        public void A_given_name_is_matched_correctly()
        {
            var elements = new List<FrameworkElement>
            {
                new FrameworkElement
                {
                    Name = "Bar",
                },
                new FrameworkElement
                {
                    Name = "Foo",
                },
            };

            FrameworkElement found = BindingScope.FindName(elements, "Foo");
            Assert.NotNull(found);
        }
    }
}
