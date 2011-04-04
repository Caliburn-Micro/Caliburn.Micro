using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Caliburn.Micro.HelloWP7
{
    public class ShowDialog<T> : IResult
    {
        Action<T> initialization = x => { };

        public IWindowManager WindowManager { get; set; }

        
        public ShowDialog<T> Init(Action<T> initialization)
        {
            this.initialization = initialization;
            return this;
        }

        public void Execute(ActionExecutionContext context)
        {
            var screen = IoC.Get<T>();
            initialization.Invoke(screen);

            Dialog = screen;
            WindowManager.ShowDialog(screen);

            var deactivated = screen as IDeactivate;
            if (deactivated == null)
                Completed(this, new ResultCompletionEventArgs());
            else
            {
                deactivated.Deactivated += (o, e) =>
                {
                    if (e.WasClosed)
                    {
                        Completed(this, new ResultCompletionEventArgs());
                    }
                };
            }
        }

        public T Dialog { get; private set; }
        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };


        
    }


}
