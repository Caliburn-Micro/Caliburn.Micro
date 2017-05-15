# Samples

This folder contains a collection of samples for Caliburn.Micro.

## [Setup](./setup)

An example of a barebones setup for each supported platform, just beyond the bare minimum (includes a container and dependency injection.) These include:

- [Windows 10 / UWP](./setup/Setup.UWP)
- [WPF](./setup/Setup.WPF)
- [Xamarin.iOS](./setup/Setup.iOS)
- [Xamarin.Android](./setup/Setup.Android)
- [Xamarin.Forms (iOS, Android, Windows Phone, Portable Class Library)](./setup/Setup.Forms)
- [Windows 8.1](./setup/Setup.Windows.Runtime)
- [Windows Phone 8.1 / Windows Runtime](./setup/Setup.WindowsPhone.Windows.Runtime)
- [Windows Phone 8.1 / Silverlight](./setup/Setup.WindowsPhone.Silverlight)
- [Silverlight](./setup/Setup.Silverlight)

## [Features](./features)

A solution that demonstrates the usage of most major framework features across all the supported platforms (showing some of the inevitable platform discrepancies). Also this solution provides an example of using a Shared project to share code (in this case the view models) across mulitple platforms. The features covered include:

- Binding Conventions
- Action Conventions
- Coroutines
- Dispatching to the UI thread
- Event Aggregation
- Design Time Conventions
- Conductors and Composition
- Navigation

## [Scenarios](./scenarios)

A collection of solutions highlighting one off scenarios that may or not apply to multiple platforms. They're such that demonstrating them on all platforms would not add extra value. These include:

- Switching IoC containers to something like Autofac
- The use of F#
- Customising the framework

