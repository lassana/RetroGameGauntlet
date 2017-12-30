using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Plugin.CurrentActivity;
using RetroGameGauntlet.Droid.Adapters;
using RetroGameGauntlet.Droid.Services;
using RetroGameGauntlet.PCL;
using RetroGameGauntlet.PCL.Adapters;
using RetroGameGauntlet.PCL.Services;
using RetroGameGauntlet.PCL.ViewModels;
using SimpleInjector;

namespace RetroGameGauntlet.Droid
{
	//You can specify additional application information in this attribute
    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          :base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            GauntletCore.Container.Register<IWebLauncherAdapter, WebLauncherAdapter>(Lifestyle.Transient);
            GauntletCore.Container.Register<IAppInfoAdapter, AppInfoAdapter>(Lifestyle.Transient);
            GauntletCore.Container.Register<IPlatformLoaderService, PlatformLoaderService>(Lifestyle.Singleton);
            GauntletCore.Container.Register<IWikipediaSearchService, WikipediaSearchService>(Lifestyle.Singleton);
            GauntletCore.Container.Register<IImageSearchService, FlickrImageSearchService>(Lifestyle.Singleton);
            GauntletCore.Container.Register<AboutViewModel>(Lifestyle.Transient);

            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);
            //A great place to initialize Xamarin.Insights and Dependency Services!
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }
    }
}