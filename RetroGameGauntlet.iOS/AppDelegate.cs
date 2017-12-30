using Foundation;
using UIKit;
using RetroGameGauntlet.PCL;
using RetroGameGauntlet.PCL.Services;
using RetroGameGauntlet.iOS.Services;
using RetroGameGauntlet.PCL.ViewModels;
using SimpleInjector;
using RetroGameGauntlet.PCL.Adapters;
using RetroGameGauntlet.iOS.Adapters;

namespace RetroGameGauntlet.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            GauntletCore.Container.Register<IWebLauncherAdapter, SafariLauncherAdapter>(Lifestyle.Transient);
            GauntletCore.Container.Register<IAppInfoAdapter, AppInfoAdapter>(Lifestyle.Transient);
            GauntletCore.Container.Register<IPlatformLoaderService, PlatformLoaderService>(Lifestyle.Singleton);
            GauntletCore.Container.Register<IWikipediaSearchService, WikipediaSearchService>(Lifestyle.Singleton);
            GauntletCore.Container.Register<IImageSearchService, FlickrImageSearchService>(Lifestyle.Singleton);
            GauntletCore.Container.Register<AboutViewModel>(Lifestyle.Transient);

            global::Xamarin.Forms.Forms.Init();

            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);


            LoadApplication(new GauntletApp());

            return base.FinishedLaunching(uiApplication, launchOptions);
        }
    }
}