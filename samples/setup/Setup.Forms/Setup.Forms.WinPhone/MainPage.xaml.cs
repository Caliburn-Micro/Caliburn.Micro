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

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new Setup.Forms.App(IoC.Get<PhoneContainer>()));
        }
    }
}
