# Setting up a new Avalonia project

1. File > New Project > `New Avalonia Application`.
2. Add nuget package `Caliburn.Micro.Avalonia` (or add references to `Caliburn.Micro.Core.dll` and `Caliburn.Micro.Avalonia.dll` from the `Caliburn.Micro.Avalonia` project)
3. Add nuget packages `Avalonia.Markup.Xaml.Loader`, `Avalonia.Xaml.Interactivity` and `Avalonia.Xaml.Behaviors`.
4. Delete `MainWindow.axaml`.
5. Create the `Views` and `ViewModels` folders.
6. Add new class `ShellViewModel` to the `ViewModels` folder.
7. Add new window `ShellView` to the `Views` folder.
8. Add new class `Bootstrapper` to the root folder.
9. Replace body of `OnFrameworkInitializationCompleted` method of `Application` class in `App.axaml.cs` to `new Bootstrapper();`

## Troubleshooting:

### Main window doesn't appear

Make sure you have added the following nuget packages:
* `Avalonia.Markup.Xaml.Loader`
* `Avalonia.Xaml.Interactivity` 
* `Avalonia.Xaml.Behaviors`
