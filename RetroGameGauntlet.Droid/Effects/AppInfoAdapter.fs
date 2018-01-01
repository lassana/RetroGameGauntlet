namespace RetroGameGauntlet.Droid.Adapters

open System
open Android.App
open Android.Content.PM
open RetroGameGauntlet.Core.Adapters

type AppInfoAdapter () =
    interface IAppInfoAdapter with
        member this.VersionName = Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName, 
                                                                                     PackageInfoFlags.MetaData).VersionName;

        member this.VersionCode = Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName,
                                                                                     PackageInfoFlags.MetaData).VersionCode.ToString();