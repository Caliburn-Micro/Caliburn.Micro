namespace Caliburn.Micro.HelloWP7 {
    using System;

    public class ShowDialog<T> : IResult {
        Action<T> initialization = x => { };

        public IWindowManager WindowManager { get; set; }


        public ShowDialog<T> Init(Action<T> initialization) {
            this.initialization = initialization;
            return this;
        }

        public void Execute(ActionExecutionContext context) {
            var screen = IoC.Get<T>();
            initialization.Invoke(screen);

            Dialog = screen;
            WindowManager.ShowDialog(screen);

            var deactivated = screen as IDeactivate;
            if(deactivated == null)
                Completed(this, new ResultCompletionEventArgs());
            else {
                deactivated.Deactivated += (o, e) => {
                    if(e.WasClosed) {
                        Completed(this, new ResultCompletionEventArgs());
                    }
                };
            }
        }

        public T Dialog { get; private set; }
        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}