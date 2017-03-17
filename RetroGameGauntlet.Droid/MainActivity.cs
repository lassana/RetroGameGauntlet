using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using RetroGameGauntlet.Forms;
using RetroGameGauntlet.Forms.Services;
using SimpleInjector;
using RetroGameGauntlet.Droid.Services;

namespace RetroGameGauntlet.Droid
{
    [Activity(Label = "RetroGameGauntlet.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            ToolbarResource = Resource.Layout.toolbar;
            TabLayoutResource = Resource.Layout.tabs;

            base.OnCreate(savedInstanceState);

            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            RetroGameGauntletApp.Container.Register<IPlatformLoaderService, PlatformLoaderService>(Lifestyle.Singleton);
            LoadApplication(new RetroGameGauntletApp());
        }
    }
}

