namespace RetroGameGauntlet.Core.ViewModels

open System
open Plugin.Share
open Plugin.Share.Abstractions
open RetroGameGauntlet.Core.Adapters
open RetroGameGauntlet.Core.Models
open RetroGameGauntlet.Core.Services
open RetroGameGauntlet.Core.ViewModels.Items
open System.Collections.Generic
open System.Diagnostics
open System.Windows.Input
open Xamarin.Forms

/// The "Overview" page view model.
type OverviewViewModel(loaderService: IPlatformLoaderService,
                       imageSearchService: IImageSearchService,
                       wikiSearchService: IWikipediaSearchService,
                       webLauncherAdapter: IWebLauncherAdapter) =
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

    let searchClickCommand: ICommand = new Command(fun () -> 
        if (String.IsNullOrEmpty(selectedGameName) || String.IsNullOrEmpty(selectedPlatformName)) |> not then 
            let google = String.Format("https://www.google.com/search?ie=utf-8&oe=utf-8&q={0} {1}", selectedGameName, selectedPlatformName)
            webLauncherAdapter.OpenUri(google) ) :> ICommand

    let mutable title: string = "Loading..."
    let mutable logoImageSource: ImageSource = null
    let mutable description: string = System.String.Empty
    let mutable searchButtonText: string = "Find more"
    let mutable wikipediaItems: IEnumerable<WikipediaItemViewModel> = Seq.empty

    let eventInitialized = new Event<_>()

    /// The page title.
    member public this.Title
        with get() = title
        and set parameter =
            title <- parameter
            this.NotifyPropertyChanged <@ this.Title @>

    /// A member shows if the current platform (e.g. Android) supports clipboarding.
    member public this.IsClipboardEnabled: bool = CrossShare.Current.SupportsClipboard

    /// The game logo image source.
    member public this.LogoImageSource
        with get() = logoImageSource
        and set parameter =
            logoImageSource <- parameter
            this.NotifyPropertyChanged <@ this.LogoImageSource @>

    /// The game description.
    member public this.Description
        with get() = description
        and set parameter =
            description <- parameter
            this.NotifyPropertyChanged <@ this.Description @>

    /// The "Search" button text.
    member public this.SearchButtonText
        with get() = searchButtonText
        and set parameter =
            searchButtonText <- parameter
            this.NotifyPropertyChanged <@ this.SearchButtonText @>

    /// The "Share game" button command.
    member public this.ShareClickCommand = shareClickCommand

    /// The "Copy to clipboard" command.
    member public this.CopyToClipboardClickCommand = copyToClipboardClickCommand

    /// The "Search" button command.
    member public this.SearchClickCommand = searchClickCommand

    /// The "Wikipeadia" listview source.
    member public this.WikipediaItems
        with get() = wikipediaItems
        and set parameter =
            wikipediaItems <- parameter
            this.NotifyPropertyChanged <@ this.WikipediaItems @>
    
    /// An event fires when the ViewModel is initialized.
    [<CLIEvent>]
    member public this.Initialized = eventInitialized.Publish

    /// Inits the game info according to the selectd platform.
    member public this.InitAsync (targetPlatform: PlatformItemViewModel) : Async<unit> =
        async {
            Debug.WriteLine("OverviewViewModel: Initializing...")
            let! newGameName = loaderService.GetRandomGameAsync(targetPlatform.PlatformModel.FileName)
            let gameName = newGameName
            let platformName = targetPlatform.Title
            return! this.InitGameAsync(gameName, platformName)
        }
    
    /// Inits the game info according to the selected game.
    member public this.InitAsync (targetGame: GameItemViewModel) : Async<unit> =
        async {
            Debug.WriteLine("OverviewViewModel: Initializing...")
            let gameName = targetGame.Game
            let platformName = targetGame.Platform
            return! this.InitGameAsync(gameName, platformName)
        }

    /// Inits the Wikipedia listview source.
    member public this.InitWikipediaItemsAsync () : Async<unit> =
        async {
            let! items = wikiSearchService.GetItemsForQueryAsync selectedGameName
            this.WikipediaItems <- items |> Seq.map(fun mdl -> WikipediaItemViewModel(mdl))
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

    member public this.Handle_WikipediaItemTapped (item: WikipediaItemViewModel) =
        webLauncherAdapter.OpenUri(item.Url)