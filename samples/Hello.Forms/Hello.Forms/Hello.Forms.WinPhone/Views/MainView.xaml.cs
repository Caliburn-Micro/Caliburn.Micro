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

            LoadApplication(new Forms.App(IoC.Get<PhoneContainer>()));
        }
    }
}
