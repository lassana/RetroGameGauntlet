using Foundation;
using RetroGameGauntlet.PCL.Adapters;
using SafariServices;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XFApplication = Xamarin.Forms.Application;

namespace RetroGameGauntlet.iOS.Adapters
{
    public class SafariLauncherAdapter : IWebLauncherAdapter
    {
        public SafariLauncherAdapter()
        {
        }

        public void OpenUri(string url)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
            {
                #pragma warning disable XI0002 // Notifies you from using newer Apple APIs when targeting an older OS version
                var safari = new SFSafariViewController(new NSUrl(url))
                {
                    ModalPresentationStyle = UIModalPresentationStyle.Popover,
                };

                if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
                {
                    safari.PreferredBarTintColor = ((Color)XFApplication.Current.Resources["backgroundColor"]).ToUIColor();
                    safari.PreferredControlTintColor = ((Color)XFApplication.Current.Resources["textColor"]).ToUIColor();
                }

                UIApplication.SharedApplication
                             .KeyWindow
                             .RootViewController
                             .PresentViewController(safari, animated: true, completionHandler: null);
                #pragma warning restore XI0002 // Notifies you from using newer Apple APIs when targeting an older OS version
            }
            else
            {
                #pragma warning disable XI0003 // Notifies you when using a deprecated, obsolete or unavailable Apple API
                UIApplication.SharedApplication.OpenUrl(new NSUrl(url));
                #pragma warning restore XI0003 // Notifies you when using a deprecated, obsolete or unavailable Apple API
            }
        }
    }
}
