namespace RetroGameGauntlet.Droid.Adapters

open Android.Support.CustomTabs
open Android.Support.V4.Content
open Plugin.CurrentActivity
open RetroGameGauntlet.Droid
open RetroGameGauntlet.Core.Adapters

type WebLauncherAdapter () =
    let activity = CrossCurrentActivity.Current.Activity

    interface IWebLauncherAdapter with
        member this.OpenUri url = 
            let builder = new CustomTabsIntent.Builder()
            builder.SetToolbarColor(ContextCompat.GetColor(activity, Resources.Color.primary)) |> ignore
            let customTabsIntent = builder.Build()
            customTabsIntent.LaunchUrl(activity, Android.Net.Uri.Parse(url))