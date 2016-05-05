using System;

using Xamarin.Forms;
using View.Main;
using Core;

namespace RetroGameGauntlet
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = new NavigationPage(new TopPage())
            {
                BarBackgroundColor = Colors.BackgroundColor,
                BarTextColor = Color.White
            };
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

