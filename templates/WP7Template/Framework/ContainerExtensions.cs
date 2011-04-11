namespace WP7Template.Framework
{
    using System;
    using Microsoft.Phone.Controls;

    public static class ContainerExtensions
	{
        public static void RegisterSingleton<TService, TImplementation>(this SimpleContainer container)
            where TImplementation : TService
        {
            container.RegisterSingleton(typeof(TService), null, typeof(TImplementation));
        }

        public static void RegisterPerRequest<TService, TImplementation>(this SimpleContainer container)
            where TImplementation : TService
        {
            container.RegisterPerRequest(typeof(TService), null, typeof(TImplementation));
        }

        public static void RegisterPerRequestPageVM<TViewModel>(this SimpleContainer container)
        {
            container.RegisterPerRequestPageVM(typeof(TViewModel));
        }

        public static void RegisterPerRequestPageVM(this SimpleContainer container, Type viewModelType)
        {
            container.RegisterPerRequest(viewModelType, viewModelType.Name, viewModelType);
        }

        public static void RegisterInstance<TService>(this SimpleContainer container, TService instance)
        {
            container.RegisterInstance(typeof(TService), null, instance);
        }

        public static void RegisterAllViewModelsForPages(this SimpleContainer container)
        {
            var pages = from assembly in AssemblySource.Instance
                        from type in assembly.GetTypes()
                        where typeof(PhoneApplicationPage).IsAssignableFrom(type)
                        select type;

            foreach (var page in pages)
            {
                var key = ViewModelLocator.DetermineKeyForViewType(page);

                var viewModelType = (from assembly in AssemblySource.Instance
                                     from type in assembly.GetTypes()
                                     where type.Name == key
                                     select type).FirstOrDefault();

                if (viewModelType != null)
                    container.RegisterPerRequestPageVM(viewModelType);
            }
        }
	}
}