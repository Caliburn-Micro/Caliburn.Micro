namespace Caliburn.Micro {
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Phone.Controls;

    public static class ContainerExtensions {
        public static SimpleContainer Singleton<TImplementation>(this SimpleContainer container) {
            container.RegisterSingleton(typeof(TImplementation), null, typeof(TImplementation));
            return container;
        }

        public static SimpleContainer Singleton<TService, TImplementation>(this SimpleContainer container)
            where TImplementation : TService {
            container.RegisterSingleton(typeof(TService), null, typeof(TImplementation));
            return container;
        }

        public static SimpleContainer PerRequest<TService, TImplementation>(this SimpleContainer container)
            where TImplementation : TService {
            container.RegisterPerRequest(typeof(TService), null, typeof(TImplementation));
            return container;
        }

        public static SimpleContainer PerRequest<TImplementation>(this SimpleContainer container) {
            container.RegisterPerRequest(typeof(TImplementation), null, typeof(TImplementation));
            return container;
        }

        public static SimpleContainer Instance<TService>(this SimpleContainer container, TService instance) {
            container.RegisterInstance(typeof(TService), null, instance);
            return container;
        }

        public static SimpleContainer Handler<TService>(this SimpleContainer container, Func<object> handler) {
            container.RegisterHandler(typeof(TService), null, handler);
            return container;
        }

        public static void RegisterPerRequestPageVM<TViewModel>(this SimpleContainer container) {
            container.RegisterPerRequestPageVM(typeof(TViewModel));
        }

        public static void RegisterPerRequestPageVM(this SimpleContainer container, Type viewModelType) {
            container.RegisterPerRequest(viewModelType, viewModelType.Name, viewModelType);
        }

        public static SimpleContainer AllTypesOf<TService>(this SimpleContainer container, Assembly assembly, Func<Type, bool> filter = null) {
            if(filter == null)
                filter = type => true;

            var types = from type in assembly.GetTypes()
                        where typeof(TService).IsAssignableFrom(type)
                              && !type.IsAbstract
                              && !type.IsInterface
                              && filter(type)
                        select type;

            foreach(var type in types) {
                container.RegisterSingleton(typeof(TService), null, type);
            }

            return container;
        }

        public static void RegisterAllViewModelsForPages(this SimpleContainer container) {
            var pages = from assembly in AssemblySource.Instance
                        from type in assembly.GetTypes()
                        where typeof(PhoneApplicationPage).IsAssignableFrom(type)
                        select type;

            foreach(var page in pages) {
                var key = ViewModelLocator.DetermineKeyForViewType(page);

                var viewModelType = (from assembly in AssemblySource.Instance
                                     from type in assembly.GetTypes()
                                     where type.Name == key
                                     select type).FirstOrDefault();

                if(viewModelType != null)
                    container.RegisterPerRequestPageVM(viewModelType);
            }
        }
    }
}