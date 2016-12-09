using System;
using System.Text.RegularExpressions;

namespace Caliburn.Micro.WPF.Tests {
    using Xunit;

    public class RegExHelperTests {
        [Fact]
        public void GetCaptureGroupReturnsGroupNameAndRegex() {
            var groupName = "group";
            var regex = ".*";

            var result = RegExHelper.GetCaptureGroup(groupName, regex);

            Assert.Equal("(?<group>.*)", result);
        }

        [Fact]
        public void ShouldEscapeDotsInNamespace() {
            var ns = "Caliburn.Micro.WPF.Tests";

            var result = RegExHelper.NamespaceToRegEx(ns);

            Assert.Equal(@"Caliburn\.Micro\.WPF\.Tests", result);
        }

        [Fact]
        public void ShouldInsertRegexWhenNamespaceHasAsterix() {
            var ns = "Caliburn.*.WPF.Tests";
            
            var result = RegExHelper.NamespaceToRegEx(ns);

            Assert.Equal(@"Caliburn\.([\p{Lu}\p{Ll}\p{Lt}\p{Lm}\p{Lo}\p{Nl}_][\p{Lu}\p{Ll}\p{Lt}\p{Lm}\p{Lo}\p{Nl}\p{Mn}\p{Mc}\p{Nd}\p{Pc}\p{Cf}_]*\.)*WPF\.Tests", result);
        }

        [Fact]
        public void GetNameCaptureGroupShouldUseNameRegEx() {
            var groupName = "group";

            var result = RegExHelper.GetNameCaptureGroup(groupName);

            Assert.Equal(@"(?<group>[\p{Lu}\p{Ll}\p{Lt}\p{Lm}\p{Lo}\p{Nl}_][\p{Lu}\p{Ll}\p{Lt}\p{Lm}\p{Lo}\p{Nl}\p{Mn}\p{Mc}\p{Nd}\p{Pc}\p{Cf}_]*)", result);
        }

        [Fact]
        public void GetNameCaptureGroupShouldUseNameSpaceRegEx()
        {
            var groupName = "group";

            var result = RegExHelper.GetNamespaceCaptureGroup(groupName);

            Assert.Equal(@"(?<group>([\p{Lu}\p{Ll}\p{Lt}\p{Lm}\p{Lo}\p{Nl}_][\p{Lu}\p{Ll}\p{Lt}\p{Lm}\p{Lo}\p{Nl}\p{Mn}\p{Mc}\p{Nd}\p{Pc}\p{Cf}_]*\.)*)", result);
        }

        [Fact]
        public void NameRegExMatchesValidCSharpIdentifiers() {

            var expression = String.Concat("^", RegExHelper.NameRegEx, "$"); // Make sure we're doing a whole string match

            Assert.True(Regex.IsMatch("identifier1", expression));
            Assert.True(Regex.IsMatch("_identifier2", expression));
            Assert.True(Regex.IsMatch("Expression_ConditionⅩExpression〡Expression〡ExpressionⅩViewModel", expression));

            Assert.False(Regex.IsMatch("1identifier", expression));
        }
    }
}