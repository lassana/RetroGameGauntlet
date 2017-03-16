using Foundation;
using UIKit;
using RetroGameGauntlet.Forms;

namespace RetroGameGauntlet.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);

            LoadApplication(new RetroGameGauntletApp());

            return base.FinishedLaunching(app, options);
        }
    }
}

