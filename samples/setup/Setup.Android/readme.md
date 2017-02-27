# Setting up a new Xamarin.Android project

1. File > New Project > `New Blank App (Android)`.
2. Add nuget package `Caliburn.Micro`.
3. Delete `MainActivity`.
4. Create `ViewModels` folder.
5. Create new class `MainViewModel`.
6. Add new empty storyboard `Views.storyboard` to the root folder.
7. Add a new view controller to the storyboard.
8. Set property `Class` on the view controller to `MainViewController`.
9. In Project properties > iOS Application > set Main Interface to `Views.storyboard`.