using System.Linq;

namespace Caliburn.Micro.HelloWindowManagerWP71 {
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;
    using Microsoft.Phone.Controls;

    public class AppBootstrapper : PhoneBootstrapperBase {
        PhoneContainer container;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure() {
            container = new PhoneContainer();

            if (!Execute.InDesignMode)
                container.RegisterPhoneServices(RootFrame);

            container.Instance<Func<MessageViewModel>>(() => new MessageViewModel());
            container.PerRequest<MainPageViewModel>();
            container.PerRequest<MessageViewModel, MessageViewModel>();
            container.PerRequest<DialogViewModel, DialogViewModel>();

            AddCustomConventions();
        }

        static void AddCustomConventions() {
            ConventionManager.AddElementConvention<Pivot>(Pivot.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
                (viewModelType, path, property, element, convention) => {
                    if(ConventionManager
                        .GetElementConvention(typeof(ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention)) {
                        ConventionManager
                            .ConfigureSelectedItem(element, Pivot.SelectedItemProperty, viewModelType, path);
                        ConventionManager
                            .ApplyHeaderTemplate(element, Pivot.HeaderTemplateProperty, null, viewModelType);
                        return true;
                    }

                    return false;
                };

            ConventionManager.AddElementConvention<Panorama>(Panorama.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
                (viewModelType, path, property, element, convention) => {
                    if(ConventionManager
                        .GetElementConvention(typeof(ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention)) {
                        ConventionManager
                            .ConfigureSelectedItem(element, Panorama.SelectedItemProperty, viewModelType, path);
                        ConventionManager
                            .ApplyHeaderTemplate(element, Panorama.HeaderTemplateProperty, null, viewModelType);
                        return true;
                    }

                    return false;
                };
        }

        protected override object GetInstance(Type service, string key) {
            var instance = container.GetInstance(service, key);
            if (instance != null)
                return instance;

            throw new Exception("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) {
            container.BuildUp(instance);
        }
    }
}