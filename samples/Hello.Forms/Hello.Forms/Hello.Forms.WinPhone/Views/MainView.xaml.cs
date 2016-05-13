using System;
using Caliburn.Micro;
using Microsoft.Phone.Controls;

namespace Hello.Forms.WinPhone.Views
{
    public partial class MainView
    {
        public MainView()
        {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

            Xamarin.Forms.Forms.Init();

            LoadApplication(IoC.Get<Forms.App>());
        }
    }
}
