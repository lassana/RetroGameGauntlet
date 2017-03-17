using RetroGameGauntlet.Forms.Views;
using SimpleInjector;
using Xamarin.Forms;
using RetroGameGauntlet.Forms.Services;
using RetroGameGauntlet.Forms.ViewModels;

namespace RetroGameGauntlet.Forms
{
    public class RetroGameGauntletApp : Application
    {
        private static Container _container;
        public static Container Container 
        {
            get 
            {
                _container = _container ?? new Container();
                return _container;
            }
        }

        public RetroGameGauntletApp()
        {
            Container.Register<IImageSearchService, FlickrImageSearchService>(Lifestyle.Singleton);
            Container.Register<IWikipediaSearchService, WikipediaSearchService>(Lifestyle.Singleton);
            Container.Register<AboutViewModel>();
            Container.Register<SearchPlatformsViewModel>();
            Container.Register<OverviewViewModel>();

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
