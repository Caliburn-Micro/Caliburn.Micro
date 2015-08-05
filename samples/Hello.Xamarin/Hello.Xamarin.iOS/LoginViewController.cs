using System;
using Hello.Xamarin.Core.Framework;
using Hello.Xamarin.Core.ViewModels;
using Hello.Xamarin.iOS.Framework;
using UIKit;

namespace Hello.Xamarin.iOS
{
	public partial class LoginViewController : UIViewControllerBase<LoginViewModel>
	{
		public LoginViewController (IntPtr handle) 
            : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SignIn.TouchUpInside += (s, e) => ViewModel.Login();

            EventHandler toggleSignIn = (s, e) =>
            {
                ViewModel.Username = Username.Text;
                ViewModel.Password = Password.Text;

                SignIn.Enabled = ViewModel.CanLogin;
            };

            Username.EditingChanged += toggleSignIn;
            Password.EditingChanged += toggleSignIn;

            ViewModel.OnChanged(v => v.Feedback, () => Feedback.Text = ViewModel.Feedback);
        }
	}
}
