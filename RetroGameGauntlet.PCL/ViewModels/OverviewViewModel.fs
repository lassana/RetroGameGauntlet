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

/// The "Overview" page view model.
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

    /// The page title.
    member this.Title
        with get() = title
        and set parameter =
            title <- parameter
            this.NotifyPropertyChanged <@ this.Title @>

    /// A member shows if the current platform (e.g. Android) supports clipboarding.
    member this.IsClipboardEnabled: bool = CrossShare.Current.SupportsClipboard

    /// The game logo image source.
    member this.LogoImageSource
        with get() = logoImageSource
        and set parameter =
            logoImageSource <- parameter
            this.NotifyPropertyChanged <@ this.LogoImageSource @>

    /// The game description.
    member this.Description
        with get() = description
        and set parameter =
            description <- parameter
            this.NotifyPropertyChanged <@ this.Description @>

    /// The "Search" button text.
    member this.SearchButtonText
        with get() = searchButtonText
        and set parameter =
            searchButtonText <- parameter
            this.NotifyPropertyChanged <@ this.Description @>

    /// The "Share game" button command.
    member this.ShareClickCommand = shareClickCommand

    /// The "Copy to clipboard" command.
    member this.CopyToClipboardClickCommand = copyToClipboardClickCommand

    /// The "Search" button command.
    member this.SearchClickCommand = searchClickCommand

    /// The "Wikipeadia" listview source.
    member this.WikipediaItems
        with get() = wikipediaItems
        and set parameter =
            wikipediaItems <- parameter
            this.NotifyPropertyChanged <@ this.Description @>
    
    /// An event fires when the ViewModel is initialized.
    [<CLIEvent>]
    member public this.Initialized = eventInitialized.Publish

    /// Inits the game info according to the selectd platform.
    member public this.InitAsync (targetPlatform: PlatformItemViewModel) : Async<unit> =
        async {
            Debug.WriteLine("OverviewViewModel: Initializing...")
            let! newGameName = loaderService.GetRandomGameAsync(targetPlatform.PlatformModel.FileName) |> Async.AwaitTask
            let gameName = newGameName
            let platformName = targetPlatform.Title
            return! this.InitGameAsync(gameName, platformName)
        }
    
    /// Inits the game info according to the selected game.
    member public this.InitAsync (targetGame: KeyValuePair<string, string>) : Async<unit> =
        async {
            Debug.WriteLine("OverviewViewModel: Initializing...")
            let gameName = targetGame.Key
            let platformName = targetGame.Value
            return! this.InitGameAsync(gameName, platformName)
        }

    /// Inits the Wikipedia listview source.
    member public this.InitWikipediaItemsAsync () : Async<unit> =
        async {
            let! items = wikiSearchService.GetItemsForQueryAsync selectedGameName
            this.WikipediaItems <- items
        }

    /// Inits the game info according to the selected platform and game.
    member private this.InitGameAsync (gameName: string, platformName: string) : Async<unit> =
        async {
            let! image = imageSearchService.GetImageForGameAsync gameName platformName
            Debug.WriteLine("The Flickr link is " + image)
            if String.IsNullOrEmpty image |> not then
                this.LogoImageSource <- UriImageSource(Uri=Uri(image))
            selectedGameName <- gameName
            selectedPlatformName <- platformName
            this.Title <- gameName
            this.Description <- String.Format("Your {0} game", platformName)
            this.SearchButtonText <- String.Format("Search for \"{0} {1}\"", platformName, gameName)
            eventInitialized.Trigger(this, EventArgs.Empty)
        }