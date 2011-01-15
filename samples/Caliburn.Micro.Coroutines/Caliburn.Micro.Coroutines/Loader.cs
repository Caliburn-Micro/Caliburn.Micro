namespace Caliburn.Micro.Coroutines
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class Loader : IResult
    {
        readonly string message;
        readonly bool hide;

        public Loader(string message)
        {
            this.message = message;
        }

        public Loader(bool hide)
        {
            this.hide = hide;
        }

        public void Execute(ActionExecutionContext context)
        {
            var view = context.View as FrameworkElement;
            while(view != null)
            {
                var busyIndicator = view as BusyIndicator;
                if(busyIndicator != null)
                {
                    if(!string.IsNullOrEmpty(message))
                        busyIndicator.BusyContent = message;
                    busyIndicator.IsBusy = !hide;
                    break;
                }

                view = view.Parent as FrameworkElement;
            }

            Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };

        public static IResult Show(string message = null)
        {
            return new Loader(message);
        }

        public static IResult Hide()
        {
            return new Loader(true);
        }
    }
}