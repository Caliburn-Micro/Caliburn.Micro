namespace WP7Template.Framework {
    using System;

    public class PhoneContainer : SimpleContainer {
        public PhoneContainer() {
            Activator = new InstanceActivator(type => GetInstance(type, null));
        }

        public InstanceActivator Activator { get; private set; }

        protected override object ActivateInstance(Type type, object[] args) {
            return Activator.ActivateInstance(base.ActivateInstance(type, args));
        }
    }
}