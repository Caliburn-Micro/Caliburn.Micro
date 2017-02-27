# Setting up a new Silverlight project

1. File > New Project > `New Silverlight Application`.
2. Add nuget package `Caliburn.Micro`.
3. Delete `MainPage.xaml`.
4. Add the `Views` and `ViewModels` folders.
5. Add new class `ShellViewModel` to the `ViewModels` folder.
6. Add user control `ShellView` to the `Views` folder.
7. Add new class `Bootstrapper` to the root folder.
8. Remove all code from `App.xaml.cs`.
9. Add `Bootstrapper` as a resource tp `App.xaml`.