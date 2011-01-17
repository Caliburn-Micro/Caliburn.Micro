namespace GameLibrary.Framework {
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using Caliburn.Micro;

    public class BusyResult : IResult {
        readonly bool hide;

        public BusyResult(bool hide) {
            this.hide = hide;
        }

        public void Execute(ActionExecutionContext context) {
            var view = context.View as FrameworkElement;

            //bottom up search
            while(view != null) {
                var indicator = view as BusyIndicator;
                if(indicator != null) {
                    indicator.IsBusy = !hide;
                    break;
                }
                view = view.Parent as FrameworkElement;
            }

            //top down search
            if(view == null) {
                var queue = new Queue<FrameworkElement>();
                queue.Enqueue(App.Current.RootVisual as FrameworkElement);

                while(queue.Count > 0) {
                    var current = queue.Dequeue();
                    if(current == null)
                        continue;

                    var indicator = current as BusyIndicator;
                    if(indicator != null) {
                        indicator.IsBusy = !hide;
                        break;
                    }

                    var count = VisualTreeHelper.GetChildrenCount(current);
                    for(var i = 0; i < count; i++) {
                        queue.Enqueue(VisualTreeHelper.GetChild(current, i) as FrameworkElement);
                    }
                }
            }

            Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}