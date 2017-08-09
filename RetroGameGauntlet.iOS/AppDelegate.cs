using Foundation;
using UIKit;
using RetroGameGauntlet.PCL;
using RetroGameGauntlet.PCL.Services;
using RetroGameGauntlet.iOS.Services;
using RetroGameGauntlet.PCL.ViewModels;
using SimpleInjector;

namespace RetroGameGauntlet.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            global::Xamarin.Forms.Forms.Init();

            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);

            RetroGameGauntletCore.Container.Register<IPlatformLoaderService, PlatformLoaderService>(Lifestyle.Singleton);
            RetroGameGauntletCore.Container.Register<IWikipediaSearchService, WikipediaSearchService>(Lifestyle.Singleton);
            RetroGameGauntletCore.Container.Register<IImageSearchService, FlickrImageSearchService>(Lifestyle.Singleton);
            RetroGameGauntletCore.Container.Register<AboutViewModel>(Lifestyle.Singleton);
            LoadApplication(new RetroGameGauntletApp());

            return base.FinishedLaunching(uiApplication, launchOptions);
        }
    }
}

