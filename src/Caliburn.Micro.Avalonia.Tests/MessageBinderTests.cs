using Xunit;

namespace Caliburn.Micro.Platform.Tests
{
    public class MessageBinderTests
    {
        [Fact]
        public void EvaluateParameterCaseInsensitive()
        {
            MessageBinder.SpecialValues.Add("$sampleParameter", context => 42);
            var caseSensitiveValue = MessageBinder.EvaluateParameter("$sampleParameter", typeof(int), new ActionExecutionContext());

            Assert.NotEqual("$sampleParameter", caseSensitiveValue);

            var caseInsensitiveValue = MessageBinder.EvaluateParameter("$sampleparameter", typeof(int), new ActionExecutionContext());
            Assert.NotEqual("$sampleparameter", caseInsensitiveValue);
        }
    }
}
