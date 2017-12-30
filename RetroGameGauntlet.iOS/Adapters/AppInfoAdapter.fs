namespace RetroGameGauntlet.iOS.Adapters

open Foundation
open RetroGameGauntlet.PCL.Adapters

type AppInfoAdapter () =
    interface IAppInfoAdapter with
        member this.VersionName = NSBundle.MainBundle.InfoDictionary.ValueForKey(new NSString("CFBundleShortVersionString")).ToString()

        member this.VersionCode = NSBundle.MainBundle.InfoDictionary.ValueForKey(new NSString("CFBundleVersion")).ToString()