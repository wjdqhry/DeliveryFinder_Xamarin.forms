using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeliveryFinder
{
    public partial class App : Application
    {
        NavigationPage mainPage = new NavigationPage(new MainPage());
        public App()
        {
            InitializeComponent();

            mainPage.BarBackgroundColor = Color.FromRgb(0x32,0xB8,0xFF);

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
