using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Caliburn.Micro.Tests.NET45
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
