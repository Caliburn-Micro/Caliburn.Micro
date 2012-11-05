using System;
using System.Threading.Tasks;

namespace Caliburn.Micro.WinRT.Sample.Results
{
    public class DelayResult : ResultBase
    {
        private readonly int _milliseconds;

        public DelayResult(int milliseconds)
        {
            _milliseconds = milliseconds;
        }

        public override async void Execute(ActionExecutionContext context)
        {
            await Task.Delay(_milliseconds);

            OnCompleted();
        }
    }
}
