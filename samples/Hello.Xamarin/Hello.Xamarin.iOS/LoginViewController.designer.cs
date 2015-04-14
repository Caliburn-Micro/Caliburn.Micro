// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Hello.Xamarin.iOS
{
	[Register ("LoginViewController")]
	partial class LoginViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel Feedback { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField Password { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton SignIn { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField Username { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (Feedback != null) {
				Feedback.Dispose ();
				Feedback = null;
			}
			if (Password != null) {
				Password.Dispose ();
				Password = null;
			}
			if (SignIn != null) {
				SignIn.Dispose ();
				SignIn = null;
			}
			if (Username != null) {
				Username.Dispose ();
				Username = null;
			}
		}
	}
}
