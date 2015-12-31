
1. New Blank App (Xamarin.Forms Portable)
2. Add Caliburn.Micro nuget to .Droid, .iOS and .WindowsPhone
3. Add Caliburn.Micro.Xamarin.Forms to all projects.
4. Add view models folder to portable.
5. Add HomeViewModel
6. Add views folder to portable
7. Add forms xaml page HomeView
8. Inherit App from FormsApplication and remove code
9. Replace LoadApplication all in MainPage.xaml
10. Remove code from App.xaml and App.xaml.cs
11. Add Bootstrapper class with SelectAssemblies
12. Add 
