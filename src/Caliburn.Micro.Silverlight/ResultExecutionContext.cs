namespace Caliburn.Micro
{
    using System.Windows;

    public class ResultExecutionContext
    {
        readonly ActionMessage message;
        readonly FrameworkElement source;
        readonly object target;
        readonly DependencyObject view;

        public ResultExecutionContext(FrameworkElement source, ActionMessage message, object target, DependencyObject view)
        {
            this.source = source;
            this.message = message;
            this.target = target;
            this.view = view;
        }

        public ActionMessage Message
        {
            get { return message; }
        }

        public FrameworkElement Source
        {
            get { return source; }
        }

        public object Target
        {
            get { return target; }
        }

        public DependencyObject View
        {
            get { return view; }
        }
    }
}