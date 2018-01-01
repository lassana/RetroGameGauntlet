namespace RetroGameGauntlet.iOS.Adapters

open Foundation
open RetroGameGauntlet.Core.Adapters
open SafariServices
open UIKit
open Xamarin.Forms
open Xamarin.Forms.Platform.iOS

type SafariLauncherAdapter () =

    interface IWebLauncherAdapter with

        member this.OpenUri url =
            let urlTextEscaped:NSString = (new NSString(url)).CreateStringByAddingPercentEscapes(NSStringEncoding.UTF8)
            let nsurl = new NSUrl(urlTextEscaped.ToString())

            if UIDevice.CurrentDevice.CheckSystemVersion(9,0) then
                let safari = new SFSafariViewController(nsurl)
                safari.ModalPresentationStyle <- UIModalPresentationStyle.Popover
                if UIDevice.CurrentDevice.CheckSystemVersion(10, 0) then
                    let background = ColorExtensions.ToUIColor (Application.Current.Resources.Item "backgroundColor" :?> Color)
                    let tint = ColorExtensions.ToUIColor (Application.Current.Resources.Item "textColor" :?> Color)
                    safari.PreferredBarTintColor <- background
                    safari.PreferredControlTintColor <- tint
                UIApplication.SharedApplication
                             .KeyWindow
                             .RootViewController
                             .PresentViewController(safari, true, null)
            else
                UIApplication.SharedApplication.OpenUrl(nsurl) |> ignore