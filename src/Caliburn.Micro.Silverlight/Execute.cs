namespace Caliburn.Micro
{
    using System;
    using System.Windows;

    public static class Execute
    {
        private static Action<System.Action> executor = action => action();

        public static void InitializeWithDispatcher()
        {
            var dispatcher = Deployment.Current.Dispatcher;

            executor = action =>{
                if(dispatcher.CheckAccess())
                    action();
                else dispatcher.BeginInvoke(action);
            };
        }

        public static void OnUIThread(this System.Action action)
        {
            executor(action);
        }
    }
}