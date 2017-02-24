# Setting up a new Windows 8.1 project

1. File > New Project > `New Blank App (Windows 8.1)`.
2. Add nuget package `Caliburn.Micro`.
3. Delete `MainPage.xaml`.
4. Create the `Views` and `ViewModels` folders.
5. Add new class `HomeViewModel` to the `ViewModels` folder.
6. Add new Blank Page `HomeView` to the `Views` folder.
7. Add a xmlns using for `Caliburn.Micro` to `App.xaml`.
8. Switch the `Application` to `CaliburnApplication`.
9. Remove all code from `App.xaml.cs`.
10. Replace with framework setup code.