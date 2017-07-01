# Setting up a new Xamarin.iOS project

1. File > New Project > `New Blank App (iOS)`.
2. Add nuget package `Caliburn.Micro`.
3. Create `ViewModels` folder.
4. Create new class `MainViewModel`.
5. Add new empty storyboard `Views.storyboard` to the root folder.
6. Add a new view controller to the storyboard.
7. Set property `Class` on the view controller to `MainViewController`.
8. In Project properties > iOS Application > set Main Interface to `Views.storyboard`.