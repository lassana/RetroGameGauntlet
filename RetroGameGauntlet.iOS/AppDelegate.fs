namespace RetroGameGauntlet.iOS

open System
open Foundation
open RetroGameGauntlet.iOS.Adapters
open RetroGameGauntlet.iOS.Services
open RetroGameGauntlet.Core
open RetroGameGauntlet.Core.Adapters
open RetroGameGauntlet.Core.Services
open SimpleInjector
open UIKit
open Xamarin.Forms

[<Register ("AppDelegate")>]
type AppDelegate () =
    inherit Xamarin.Forms.Platform.iOS.FormsApplicationDelegate ()

    // This method is invoked when the application is ready to run.
    override this.FinishedLaunching (app, options) =
        GauntletCore.Container.Register<IAppInfoAdapter, AppInfoAdapter>(Lifestyle.Transient)
        GauntletCore.Container.Register<IWebLauncherAdapter, SafariLauncherAdapter>(Lifestyle.Transient)
        GauntletCore.Container.Register<IPlatformLoaderService, PlatformLoaderService>(Lifestyle.Singleton)
        GauntletCore.Container.Register<IWikipediaSearchService, WikipediaSearchService>(Lifestyle.Singleton)
        GauntletCore.Container.Register<IImageSearchService, FlickrImageSearchService>(Lifestyle.Singleton)

        UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false)
        Forms.Init()

        base.LoadApplication(GauntletApp())

        base.FinishedLaunching(app, options)