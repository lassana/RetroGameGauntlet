using RetroGameGauntlet.Forms.Views;
using Xamarin.Forms;

namespace RetroGameGauntlet.Forms
{
    public class RetroGameGauntletApp : Application
    {
        public RetroGameGauntletApp()
        {
            Resources = new ResourceDictionary();
            Resources.Add("not", new ValueConverters.BooleanNegationConverter());
            Resources.Add("backgroundColor", Color.FromHex("#444444"));
            Resources.Add("backgroundColorLighter", Color.FromHex("#888888"));

            MainPage = new NavigationPage(new RootPage())
            {
                BarBackgroundColor = (Color)Resources["backgroundColor"],
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
