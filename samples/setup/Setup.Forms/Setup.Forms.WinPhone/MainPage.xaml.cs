using System;
using Caliburn.Micro;
using Microsoft.Phone.Controls;

namespace Setup.Forms.WinPhone
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

            Xamarin.Forms.Forms.Init();
            LoadApplication(IoC.Get<Forms.App>());
        }
    }
}
