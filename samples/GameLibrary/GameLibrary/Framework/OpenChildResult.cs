namespace GameLibrary.Framework {
    using System;
    using Caliburn.Micro;

    public class OpenChildResult<TChild> : OpenResultBase<TChild> {
        readonly Func<ActionExecutionContext, TChild> locateChild = c => IoC.Get<TChild>();
        Func<ActionExecutionContext, IConductor> locateParent = c => (IConductor)c.Target;

        public OpenChildResult() {}

        public OpenChildResult(TChild child) {
            locateChild = c => child;
        }

        public OpenChildResult<TChild> In<TParent>()
            where TParent : IConductor {
            locateParent = c => IoC.Get<TParent>();
            return this;
        }

        public OpenChildResult<TChild> In(IConductor parent) {
            locateParent = c => parent;
            return this;
        }

        public override void Execute(ActionExecutionContext context) {
            var parent = locateParent(context);
            var child = locateChild(context);

            if(onConfigure != null)
                onConfigure(child);

            EventHandler<ActivationProcessedEventArgs> processed = null;
            processed = (s, e) => {
                parent.ActivationProcessed -= processed;

                if(e.Success) {
                    OnOpened(parent, child);

                    var deactivator = child as IDeactivate;
                    if(deactivator != null && onClose != null) {
                        EventHandler<DeactivationEventArgs> handler = null;
                        handler = (s2, e2) => {
                            if(!e2.WasClosed)
                                return;

                            deactivator.Deactivated -= handler;
                            onClose(child);
                        };

                        deactivator.Deactivated += handler;
                    }

                    OnCompleted(null, false);
                }
                else OnCompleted(null, true);
            };

            parent.ActivationProcessed += processed;
            parent.ActivateItem(child);
        }

        protected virtual void OnOpened(IConductor parent, TChild child) {}
    }
}