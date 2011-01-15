namespace Caliburn.Micro.HelloScreens.Shell {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework;

    public class ApplicationCloseStrategy : ICloseStrategy<IWorkspace> {
        IEnumerator<IWorkspace> enumerator;
        bool finalResult;
        Action<bool, IEnumerable<IWorkspace>> callback;

        public void Execute(IEnumerable<IWorkspace> toClose, Action<bool, IEnumerable<IWorkspace>> callback) {
            enumerator = toClose.GetEnumerator();
            this.callback = callback;
            finalResult = true;

            Evaluate(finalResult);
        }

        void Evaluate(bool result)
        {
            finalResult = finalResult && result;

            if (!enumerator.MoveNext() || !result)
                callback(finalResult, new List<IWorkspace>());
            else
            {
                var current = enumerator.Current;
                var conductor = current as IConductor;
                if (conductor != null)
                {
                    var tasks = conductor.GetChildren()
                        .OfType<IHaveShutdownTask>()
                        .Select(x => x.GetShutdownTask())
                        .Where(x => x != null);

                    var sequential = new SequentialResult(tasks.GetEnumerator());
                    sequential.Completed += (s, e) => {
                        if(!e.WasCancelled)
                        Evaluate(!e.WasCancelled);
                    };
                    sequential.Execute(new ActionExecutionContext());
                }
                else Evaluate(true);
            }
        }
    }
}