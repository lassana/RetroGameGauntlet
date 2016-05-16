using Xamarin.Forms;
using RetroGameGauntlet.View;
using RetroGameGauntlet.Core;

namespace RetroGameGauntlet
{
    public class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new TopPage())
            {
                BarBackgroundColor = Colors.BackgroundColor,
                BarTextColor = Color.White
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

