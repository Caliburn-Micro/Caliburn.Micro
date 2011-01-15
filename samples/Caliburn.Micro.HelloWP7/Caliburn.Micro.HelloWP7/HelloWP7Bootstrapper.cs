namespace Caliburn.Micro.HelloWP7
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Tasks;

    public class HelloWP7Bootstrapper : PhoneBootstrapper
    {
        PhoneContainer container;

        protected override void Configure()
        {
            container = new PhoneContainer(this);

            container.RegisterPerRequest(typeof(MainPageViewModel), "MainPageViewModel", typeof(MainPageViewModel));
            container.RegisterPerRequest(typeof(PageTwoViewModel), "PageTwoViewModel", typeof(PageTwoViewModel));
            container.RegisterPerRequest(typeof(TabViewModel), null, typeof(TabViewModel));

            container.RegisterInstance(typeof(INavigationService), null, new FrameAdapter(RootFrame));
            container.RegisterInstance(typeof(IPhoneService), null, new PhoneApplicationServiceAdapter(PhoneService));

            container.Activator.InstallChooser<PhoneNumberChooserTask, PhoneNumberResult>();
            container.Activator.InstallLauncher<EmailComposeTask>();

            AddCustomConventions();
        }

        static void AddCustomConventions() {
            ConventionManager.AddElementConvention<Pivot>(Pivot.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
                (viewModelType, path, property, element, convention) => {
                    ConventionManager
                        .GetElementConvention(typeof(ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention);
                    ConventionManager
                        .ConfigureSelectedItem(element, Pivot.SelectedItemProperty, viewModelType, path);
                    ConventionManager
                        .ApplyHeaderTemplate(element, Pivot.HeaderTemplateProperty, viewModelType);
                };

            ConventionManager.AddElementConvention<Panorama>(Panorama.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
                (viewModelType, path, property, element, convention) => {
                    ConventionManager
                        .GetElementConvention(typeof(ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention);
                    ConventionManager
                        .ConfigureSelectedItem(element, Panorama.SelectedItemProperty, viewModelType, path);
                    ConventionManager
                        .ApplyHeaderTemplate(element, Panorama.HeaderTemplateProperty, viewModelType);
                };
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}