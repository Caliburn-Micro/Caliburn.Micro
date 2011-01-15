namespace Caliburn.Micro.Coroutines
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<object>, IShell
    {
        readonly ScreenOneViewModel initialialScreen;
        readonly Stack<object> previous = new Stack<object>();
        bool goingBack;

        [ImportingConstructor]
        public ShellViewModel(ScreenOneViewModel initialialScreen)
        {
            this.initialialScreen = initialialScreen;
        }

        public bool CanGoBack
        {
            get { return previous.Count > 0; }
        }

        public void GoBack()
        {
            goingBack = true;
            ActivateItem(previous.Pop());
            goingBack = false;
        }

        protected override void OnInitialize()
        {
            ActivateItem(initialialScreen);
            base.OnInitialize();
        }

        protected override void ChangeActiveItem(object newItem, bool closePrevious)
        {
            if(ActiveItem != null && !goingBack)
                previous.Push(ActiveItem);

            NotifyOfPropertyChange(() => CanGoBack);

            base.ChangeActiveItem(newItem, closePrevious);
        }
    }
}