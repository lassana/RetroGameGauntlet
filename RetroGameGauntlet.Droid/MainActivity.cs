using Android.App;
using Android.Content.PM;
using Android.OS;
using RetroGameGauntlet.PCL;
using Xamarin.Forms.Platform.Android;

namespace RetroGameGauntlet.Droid
{
    [Activity(Label = "RetroGameGauntlet.Droid",
              Icon = "@drawable/icon", 
              MainLauncher = true, 
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            ToolbarResource = Resource.Layout.toolbar;
            TabLayoutResource = Resource.Layout.tabs;

            base.OnCreate(savedInstanceState);

            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new GauntletApp());
        }
    }
}

