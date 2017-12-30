using Android.App;
using Android.Support.CustomTabs;
using Android.Support.V4.Content;
using Plugin.CurrentActivity;
using RetroGameGauntlet.PCL.Adapters;
using Uri = Android.Net.Uri;

namespace RetroGameGauntlet.Droid.Adapters
{
    public class WebLauncherAdapter : IWebLauncherAdapter
    {
        private Activity Activty => CrossCurrentActivity.Current.Activity;

        public WebLauncherAdapter()
        {
        }

        public void OpenUri(string url)
        {
            CustomTabsIntent.Builder builder = new CustomTabsIntent.Builder();
            builder.SetToolbarColor(ContextCompat.GetColor(Activty, Resource.Color.primary));
            CustomTabsIntent customTabsIntent = builder.Build();
            customTabsIntent.LaunchUrl(Activty, Uri.Parse(url));
        }
    }
}
