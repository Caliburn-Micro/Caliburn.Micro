#if !XAMARINFORMS
using System;
using Caliburn.Micro;

#if SILVERLIGHT || WPF
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
#if WPF
            if (!(context.View is FrameworkElement))
                throw new InvalidOperationException("View must be a FrameworkElement to use VisualStateResult");

            var view = (FrameworkElement) context.View;

            var success = VisualStateManager.GoToElementState(view, StateName, UseTransitions);
#else
            if (!(context.View is Control))
                throw new InvalidOperationException("View must be a Control to use VisualStateResult");

            var view = (Control)context.View;

            VisualStateManager.GoToState(view, StateName, UseTransitions);
#endif
            OnCompleted();
        }
    }
}
#endif
