# Setting up a new Windows Phone 8.1 (Silverlight) project

1. File > Mew Project > `New Blank App (Windows Phone Silverlight)`.
2. Add nuget package `Caliburn.Micro`.
3. Delete `MainPage.xaml`.
4. Create the `Views` and `ViewModels` folders.
5. Add new class `HomeViewModel` to the `ViewModels` folder.
6. Add new portrait page `HomeView` to the `Views` folder.
7. Add new class `Bootstrapper` to the root folder.
8. Set navigation page to `Views/HomeView.xaml` in `WMAppMainfest.xml`.
9. Remove all code from `App.xaml.cs` and `App.xaml`
10. Add `Bootstrapper` as a resource to `App.xaml`.