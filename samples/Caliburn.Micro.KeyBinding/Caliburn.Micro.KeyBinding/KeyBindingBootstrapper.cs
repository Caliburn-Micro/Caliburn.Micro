namespace Caliburn.Micro.KeyBinding {
    using System;
    using System.Windows.Input;
    using Input;

    public class KeyBindingBootstrapper : Bootstrapper<ShellViewModel> {
        protected override void Configure() {
            var trigger = Parser.CreateTrigger;

            Parser.CreateTrigger = (target, triggerText) => {
                if(triggerText == null) {
                    var defaults = ConventionManager.GetElementConvention(target.GetType());
                    return defaults.CreateTrigger();
                }

                var triggerDetail = triggerText
                    .Replace("[", string.Empty)
                    .Replace("]", string.Empty);

                var splits = triggerDetail.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                if(splits[0] == "Key") {
                    var key = (Key)Enum.Parse(typeof(Key), splits[1], true);
                    return new KeyTrigger { Key = key };
                }

                return trigger(target, triggerText);
            };
        }
    }
}