namespace RetroGameGauntlet.PCL.ViewModels

open RetroGameGauntlet.PCL.Adapters
open RetroGameGauntlet.PCL.Services
open System
open System.Windows.Input
open Xamarin.Forms

/// The "About" page view model.
type AboutViewModel(imgSearchService: IImageSearchService,
                    appInfoAdapter: IAppInfoAdapter,
                    webLauncherAdapter: IWebLauncherAdapter) =
    inherit ViewModelBase()
    let forkTapCommand: ICommand = 
        new Command(fun () -> webLauncherAdapter.OpenUri("https://github.com/lassana/RetroGameGauntlet")) :> ICommand
    let mutable randomImageSource: UriImageSource = null

    /// The "Fork me on Github" command.
    member this.ForkTapCommand = forkTapCommand

    /// A random Flick image.
    member this.RandomImageSource
        with get() = randomImageSource
        and set parameter =
            randomImageSource <- parameter
            this.NotifyPropertyChanged <@ this.RandomImageSource @>

    /// The app version (e.g. "1.0")
    member this.VersionName = String.Format("{0} ({1})", appInfoAdapter.VersionName, appInfoAdapter.VersionCode)

    /// Init the
    member this.InitAsync() : Async<unit> =
        async {
            let! newRandomImage = imgSearchService.GetImageAsync
            if (String.IsNullOrEmpty(newRandomImage) |> not)
                && (randomImageSource = null || newRandomImage <> randomImageSource.Uri.AbsoluteUri)
                then
                    this.RandomImageSource <- UriImageSource(Uri=Uri(newRandomImage))
        }