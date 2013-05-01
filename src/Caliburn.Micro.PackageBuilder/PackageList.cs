namespace Caliburn.Micro.PackageBuilder {
    using System.Collections.Generic;

    public class PackageList : List<PackageModel> {
        public PackageList() {
            Add(new PackageModel {
                Id = "Caliburn.Micro.Container",
                Description = "A simple IoC Container for .NET, SL, WP7, WP8, WinRT, Mono and Unity3d.",
                Tags = "Silverlight WPF WP7 WP71 WP8 SL4 SL5 SL Phone IoC DI Container Mono Unity3d WinRT Metro",
                Content = {
                    "../../../Caliburn.Micro.Silverlight/ExtensionMethods.cs",
                    "../../../Caliburn.Micro.Silverlight/ContainerExtensions.cs",
                    "../../../Caliburn.Micro.Silverlight/SimpleContainer.cs"
                }
            });

            Add(new PackageModel {
                Id = "Caliburn.Micro.EventAggregator",
                Description = "A small, simple event aggregator implementation for .NET, SL, WP7, WP8, WinRT, Mono and Unity3d.",
                Tags = "Silverlight WPF WP7 WP71 WP8 SL4 SL5 SL Phone EA PubSub EventAggregator Messaging Messenger Publish Subscribe Event Mono Unity3d WinRT Metro",
                Content = {
                    "../../../Caliburn.Micro.Silverlight/ExtensionMethods.cs",
                    "../../../Caliburn.Micro.Silverlight/EventAggregator.cs",
                }
            });

            Add(new PackageModel {
                Id = "Caliburn.Micro.INPC",
                Description = "A standalone version of Caliburn.Micro's INPC implementation, BindableCollection, Execute.OnUIThread and InDesignMode checking designed for all Xaml platforms.",
                Tags = "Silverlight WPF WP7 WP71 WP8 SL4 SL5 SL Phone WinRT INPC INotifyPropertyChanged MVVM WinRT Metro",
                Content = {
                    "../../../Caliburn.Micro.Silverlight/ExtensionMethods.cs",
                    "../../../Caliburn.Micro.Silverlight/INPC.cs",
                }
            });
        }
    }
}