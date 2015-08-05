using System;
using Android.App;
using Android.OS;
using Android.Text;
using Android.Widget;
using Hello.Xamarin.Android.Framework;
using Hello.Xamarin.Core.ViewModels;
using Hello.Xamarin.Core.Framework;

namespace Hello.Xamarin.Android.Activities
{
    [Activity(MainLauncher = true)]
    public class LoginActivity : ActivityBase<LoginViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Login);

            var userName = FindViewById<EditText>(Resource.Id.userName);
            var password = FindViewById<EditText>(Resource.Id.password);

            var button = FindViewById<Button>(Resource.Id.signIn);

            var feedback = FindViewById<TextView>(Resource.Id.feedback);

            button.Click += (s, e) => ViewModel.Login();

            EventHandler<TextChangedEventArgs> toggleSignIn = (s, e) =>
            {
                ViewModel.Username = userName.Text;
                ViewModel.Password = password.Text;

                button.Enabled = ViewModel.CanLogin;
            };

            userName.TextChanged += toggleSignIn;
            password.TextChanged += toggleSignIn;

            ViewModel.OnChanged(v => v.Feedback, () => feedback.Text = ViewModel.Feedback);
        }
    }
}