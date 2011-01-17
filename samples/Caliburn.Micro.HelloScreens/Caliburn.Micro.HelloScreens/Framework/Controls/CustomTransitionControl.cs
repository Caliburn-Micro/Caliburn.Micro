namespace Caliburn.Micro.HelloScreens.Framework.Controls {
    using System;
    using System.Windows.Controls;

    public class CustomTransitionControl : TransitioningContentControl {
        public event EventHandler ContentChanged = delegate { };

        protected override void OnContentChanged(object oldContent, object newContent) {
            base.OnContentChanged(oldContent, newContent);
            ContentChanged(this, EventArgs.Empty);
        }
    }
}