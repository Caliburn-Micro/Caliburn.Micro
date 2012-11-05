using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Caliburn.Micro.WinRT.Sample.Results
{
    public class VisualStateResult : ResultBase
    {
        private readonly string _stateName;
        private readonly bool _useTransitions;

        public VisualStateResult(string stateName, bool useTransitions = true)
        {
            _stateName = stateName;
            _useTransitions = useTransitions;
        }

        public string StateName
        {
            get { return _stateName; }
        }

        public bool UseTransitions
        {
            get { return _useTransitions; }
        }

        public override void Execute(ActionExecutionContext context)
        {
            if (!(context.View is Control))
                throw new InvalidOperationException("View must be a Control to use VisualStateResult");

            var view = (Control)context.View;

            VisualStateManager.GoToState(view, StateName, UseTransitions);

            OnCompleted();
        }
    }
}
