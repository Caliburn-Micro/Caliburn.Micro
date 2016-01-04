using System;
using Caliburn.Micro;

#if SILVERLIGHT
using System.Windows;
using System.Windows.Controls;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#endif

namespace Features.CrossPlatform.Results
{
    public class VisualStateResult : ResultBase
    {
        public VisualStateResult(string stateName, bool useTransitions = true)
        {
            StateName = stateName;
            UseTransitions = useTransitions;
        }

        public string StateName { get; }

        public bool UseTransitions { get; }

        public override void Execute(CoroutineExecutionContext context)
        {
            if (!(context.View is Control))
                throw new InvalidOperationException("View must be a Control to use VisualStateResult");

            var view = (Control) context.View;

            VisualStateManager.GoToState(view, StateName, UseTransitions);

            OnCompleted();
        }
    }
}
