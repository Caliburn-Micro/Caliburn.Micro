namespace Caliburn.Micro
{
    using System.Windows;

    public class ResultExecutionContext
    {
        public ActionMessage Message;
        public FrameworkElement Source;
        public object Target;
        public DependencyObject View;
    }
}