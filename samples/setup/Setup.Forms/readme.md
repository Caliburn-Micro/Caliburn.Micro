# Setting up a new Xamarin.Forms cross platform project

1. File > New Project > `New Blank App (Xamarin.Forms Portable)`.
2. Add nuget package `Caliburn.Micro` to the `.Droid`, `.iOS` and `.WindowsPhone` projects.
3. Add nuget package `Caliburn.Micro.Xamarin.Forms` to all projects.
4. Create the `Views` and `ViewModels` folders to the Portable class library..
5. Add new class `HomeViewModel` to the `ViewModels` folder.
6. Add new Forms (Xaml Page) `HomeView` to the `Views` folder.
7. Inherit `App` from `FormsApplication` and remove existing `code`.

## Windows Phone (Silverlight)

1. Remove all code from `App.xaml.cs` and `App.xaml`.
2. Add new class `Bootstrapper` to the root folder.
3. Add `Bootstrapper` as a resource to `App.xaml`.
4. Replace `LoadApplication` in `MainPage.xaml`.

## Xamarin.Android

1. Add new class `Application`.
2. Replace `LoadingApplication` in `MainActivity`.

## Xamarin.iOS

1. Add new class `CaliburnAppDelegate`.
2. Modify `AppDelegate`.
