namespace RetroGameGauntlet.Droid

open System
open Android.App
open Android.Content.PM
open Android.OS
open RetroGameGauntlet.Droid
open RetroGameGauntlet.Core
open Xamarin.Forms
open Xamarin.Forms.Platform.Android

[<Activity (Label = "@string/app_name", 
            MainLauncher = true,
            Icon = "@mipmap/icon")>]
type MainActivity () =
    inherit FormsAppCompatActivity ()

    override this.OnCreate (savedInstanceState: Bundle) =
        FormsAppCompatActivity.ToolbarResource <- Resources.Layout.toolbar
        FormsAppCompatActivity.TabLayoutResource <- Resources.Layout.tabs
        base.OnCreate (savedInstanceState)
        Forms.Init(this, savedInstanceState)
        this.LoadApplication(GauntletApp())