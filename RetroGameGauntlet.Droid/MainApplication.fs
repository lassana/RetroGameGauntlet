namespace RetroGameGauntlet.Droid

open System
open Android.App
open Android.OS
open Android.Runtime
open Plugin.CurrentActivity
open RetroGameGauntlet.Droid.Adapters
open RetroGameGauntlet.Droid.Services
open RetroGameGauntlet.PCL
open RetroGameGauntlet.PCL.Adapters
open RetroGameGauntlet.PCL.Services
open RetroGameGauntlet.PCL.ViewModels
open SimpleInjector

[<Application>]
type MainApplication (handle: IntPtr, transfer: JniHandleOwnership ) = 
    inherit Application (handle, transfer)

    interface Application.IActivityLifecycleCallbacks with
        member this.OnActivityCreated(activity: Activity, savedInstanceState: Bundle) =
            CrossCurrentActivity.Current.Activity <- activity

        member this.OnActivityDestroyed(activity: Activity ) = ()

        member this.OnActivityPaused(activity: Activity) = ()

        member this.OnActivityResumed(activity: Activity) =
            CrossCurrentActivity.Current.Activity <- activity

        member this.OnActivitySaveInstanceState(activity: Activity, outState: Bundle) = ()

        member this.OnActivityStarted(activity: Activity) =
            CrossCurrentActivity.Current.Activity <- activity

        member this.OnActivityStopped(activity: Activity) = ()

    override this.OnCreate() =
        GauntletCore.Container.Register<IAppInfoAdapter, AppInfoAdapter>(Lifestyle.Transient)
        GauntletCore.Container.Register<IWebLauncherAdapter, WebLauncherAdapter>(Lifestyle.Transient)
        GauntletCore.Container.Register<IPlatformLoaderService, PlatformLoaderService>(Lifestyle.Singleton);
        GauntletCore.Container.Register<IWikipediaSearchService, WikipediaSearchService>(Lifestyle.Singleton)
        GauntletCore.Container.Register<IImageSearchService, FlickrImageSearchService>(Lifestyle.Singleton)
        base.OnCreate()
        this.RegisterActivityLifecycleCallbacks(this)
    
    override this.OnTerminate () =
        base.OnTerminate()
        this.UnregisterActivityLifecycleCallbacks(this)