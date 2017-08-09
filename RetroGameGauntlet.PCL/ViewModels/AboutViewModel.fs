namespace RetroGameGauntlet.PCL.ViewModels

open RetroGameGauntlet.PCL.Services
open System
open System.Windows.Input
open Xamarin.Forms

type AboutViewModel(imgSearchService: IImageSearchService) =
    inherit ViewModelBase()
    let forkTapCommand: ICommand = 
        new Command(fun () -> Device.OpenUri(Uri("https://github.com/lassana/RetroGameGauntlet"))) :> ICommand
    let mutable randomImageSource: UriImageSource = null
    
    member this.ForkTapCommand = forkTapCommand

    member this.RandomImageSource
        with get() = randomImageSource
        and set parameter =
            randomImageSource <- parameter
            this.NotifyPropertyChanged <@ this.RandomImageSource @>

    //TODO read correct version value
    member this.VersionName = "1.0"

    member this.InitAsync =
        async {
            let! newRandomImage = imgSearchService.GetImage |> Async.AwaitTask
            if randomImageSource = null || newRandomImage <> randomImageSource.Uri.AbsoluteUri
                then
                    this.RandomImageSource <- UriImageSource(Uri=Uri(newRandomImage))
        }
        |> Async.StartAsTask