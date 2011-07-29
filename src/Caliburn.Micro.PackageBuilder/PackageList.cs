namespace Caliburn.Micro.PackageBuilder {
    using System.Collections.Generic;

    public class PackageList : List<PackageModel> {
        public PackageList() {
            Add(new PackageModel {
                Id = "Caliburn.Micro.Container",
                Description = "A simple IoC Container for .NET, Silverlight and WP7.",
                Tags = "Silverlight WPF WP7 SL4 SL Phone IoC DI Container",
                Content = {
                    "../../../Caliburn.Micro.Silverlight/ExtensionMethods.cs",
                    "../../../Caliburn.Micro.WP7.Extensions/ContainerExtensions.cs",
                    "../../../Caliburn.Micro.WP7.Extensions/SimpleContainer.cs"
                }
            });

            Add(new PackageModel {
                Id = "Caliburn.Micro.EventAggregator",
                Description = "A small, simple event aggregator implementation for .NET, Silverlight and WP7.",
                Tags = "Silverlight WPF WP7 SL4 SL Phone EA PubSub EventAggregator Messaging Messenger Publish Subscribe Event",
                Content = {
                    "../../../Caliburn.Micro.Silverlight/ExtensionMethods.cs",
                    "../../../Caliburn.Micro.Silverlight/EventAggregator.cs",
                }
            });

            Add(new PackageModel {
                Id = "Caliburn.Micro.INPC",
                Description = "A standalone version of Caliburn.Micro's INPC implementation, BindableCollection, Execute.OnUIThread and InDesignMode checking.",
                Tags = "Silverlight WPF WP7 SL4 SL Phone INPC INotifyPropertyChanged MVVM",
                Content = {
                    "../../../Caliburn.Micro.Silverlight/ExtensionMethods.cs",
                    "../../../Caliburn.Micro.Silverlight/INPC.cs",
                }
            });
        }
    }
}