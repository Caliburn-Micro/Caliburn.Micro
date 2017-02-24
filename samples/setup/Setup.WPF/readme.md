# Setting up a new WPF project

1. File > New Project > `New WPF Application`.
2. Add nuget package `Caliburn.Micro`.
3. Delete `MainWindow.xaml`.
4. Create the `Views` and `ViewModels` folders.
5. Add new class `ShellViewModel` to the `ViewModels` folder.
6. Add new window `ShellView` to the `Views` folder.
7. Add new class `Bootstrapper` to the root folder.
8. Remove `StartupUri` from the `Application` element in `App.xaml`.
9. Add `Bootstrapper` as a resource to `App.xaml`.