namespace RetroGameGauntlet.iOS.Adapters

open Foundation
open RetroGameGauntlet.PCL.Adapters
open SafariServices
open UIKit
open Xamarin.Forms
open Xamarin.Forms.Platform.iOS

type SafariLauncherAdapter () =
    interface IWebLauncherAdapter with
        member this.OpenUri url =
            if UIDevice.CurrentDevice.CheckSystemVersion(9,0) then
                let safari = new SFSafariViewController(new NSUrl(url))
                safari.ModalPresentationStyle <- UIModalPresentationStyle.Popover
                if UIDevice.CurrentDevice.CheckSystemVersion(10, 0) then
                    let background = ColorExtensions.ToUIColor (Application.Current.Resources.Item "backgroundColor" :?> Color)
                    let tint = ColorExtensions.ToUIColor (Application.Current.Resources.Item "textColor" :?> Color)
                    safari.PreferredBarTintColor <- background
                    safari.PreferredControlTintColor <- tint
            else
                UIApplication.SharedApplication.OpenUrl(new NSUrl(url)) |> ignore