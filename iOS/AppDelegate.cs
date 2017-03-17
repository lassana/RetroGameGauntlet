using Foundation;
using UIKit;
using RetroGameGauntlet.Forms;
using RetroGameGauntlet.Forms.Services;
using RetroGameGauntlet.iOS.Services;
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

            RetroGameGauntletApp.Container.Register<IPlatformLoaderService, PlatformLoaderService>(Lifestyle.Singleton);
            LoadApplication(new RetroGameGauntletApp());

            return base.FinishedLaunching(uiApplication, launchOptions);
        }
    }
}

