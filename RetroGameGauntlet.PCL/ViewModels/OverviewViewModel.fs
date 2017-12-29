namespace RetroGameGauntlet.PCL.ViewModels

open System
open Plugin.Share
open Plugin.Share.Abstractions
open RetroGameGauntlet.PCL.Models
open RetroGameGauntlet.PCL.Services
open System.Collections.Generic
open System.Diagnostics
open System.Windows.Input
open Xamarin.Forms

type OverviewViewModel(loaderService: IPlatformLoaderService,
                       imageSearchService: IImageSearchService,
                       wikiSearchService: IWikipediaSearchService) =
    inherit ViewModelBase()
    let mutable selectedGameName: string = String.Empty
    let mutable selectedPlatformName: string = String.Empty
    
    let shareMessage() = new ShareMessage(Text = String.Format("My {0} game is {1}", selectedPlatformName, selectedGameName), Title = selectedGameName)
    let shareClickCommand: ICommand = new Command(fun () -> 
                                                    CrossShare.Current.Share(shareMessage()) 
                                                    |> ignore) :> ICommand

    let clipboardText() = String.Format("My {0} game is {1}", selectedPlatformName, selectedGameName)
    let copyToClipboardClickCommand: ICommand = new Command(fun () -> 
                                                                CrossShare.Current.SetClipboardText(clipboardText())
                                                                |> ignore ) :> ICommand

    let searchClickCommand: ICommand = new Command(fun () -> Debug.WriteLine("Search")) :> ICommand

    let mutable title: string = "Loading..."
    let mutable logoImageSource: ImageSource = null
    let mutable description: string = System.String.Empty
    let mutable searchButtonText: string = "Find more"
    let mutable wikipediaItems: IEnumerable<WikipediaItemViewModel> = Seq.empty

    let eventInitialized = new Event<_>()

    member this.Title
        with get() = title
        and set parameter =
            title <- parameter
            this.NotifyPropertyChanged <@ this.Title @>

    member this.IsClipboardEnabled: bool = CrossShare.Current.SupportsClipboard

    member this.LogoImageSource
        with get() = logoImageSource
        and set parameter =
            logoImageSource <- parameter
            this.NotifyPropertyChanged <@ this.LogoImageSource @>

    member this.Description
        with get() = description
        and set parameter =
            description <- parameter
            this.NotifyPropertyChanged <@ this.Description @>

    member this.SearchButtonText
        with get() = searchButtonText
        and set parameter =
            searchButtonText <- parameter
            this.NotifyPropertyChanged <@ this.Description @>

    member this.ShareClickCommand = shareClickCommand

    member this.CopyToClipboardClickCommand = copyToClipboardClickCommand

    member this.SearchClickCommand = searchClickCommand

    member this.WikipediaItems
        with get() = wikipediaItems
        and set parameter =
            wikipediaItems <- parameter
            this.NotifyPropertyChanged <@ this.Description @>
    
    [<CLIEvent>]
    member public this.Initialized = eventInitialized.Publish

    member public this.Init (targetPlatform: PlatformItemViewModel) : Async<unit> =
        async {
            Debug.WriteLine("OverviewViewModel: Initializing...")
            let! newGameName = loaderService.GetRandomGame(targetPlatform.PlatformModel.FileName) |> Async.AwaitTask
            let gameName = newGameName
            let platformName = targetPlatform.Title
            return! this.InitGame(gameName, platformName)
        }
    
    member public this.Init (targetGame: KeyValuePair<string, string>) : Async<unit> =
        async {
            Debug.WriteLine("OverviewViewModel: Initializing...")
            let gameName = targetGame.Key
            let platformName = targetGame.Value
            return! this.InitGame(gameName, platformName)
        }

    member public this.InitListItems () : Async<unit> =
        async {
            let! items = (wikiSearchService.GetItemsForQuery selectedGameName) |> Async.AwaitTask
            this.WikipediaItems <- items
        }

    member public this.InitGame (gameName: string, platformName: string) : Async<unit> =
        async {
            let! image = (imageSearchService.GetImageForGame gameName platformName) |> Async.AwaitTask
            Debug.WriteLine("The Flickr link is " + image)
            if String.IsNullOrEmpty image |> not then
                this.LogoImageSource <- UriImageSource(Uri=Uri(image))
            selectedGameName <- gameName
            selectedPlatformName <- platformName
            this.Title <- gameName
            this.Description <- String.Format("Your {0} game", platformName)
            this.SearchButtonText <- String.Format("Search \"{0} {1}\"", platformName, gameName)
            eventInitialized.Trigger(this, EventArgs.Empty)
        }