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
    let gameName: string = String.Empty
    let platformName: string = String.Empty
    
    let shareMessage() = new ShareMessage(Text = String.Format("My {0} game is {1}", platformName, gameName), Title = gameName)
    let shareClickCommand: ICommand = new Command(fun () -> 
                                                    CrossShare.Current.Share(shareMessage()) 
                                                    |> ignore) :> ICommand

    let clipboardText() = String.Format("My {0} game is {1}", platformName, gameName)
    let copyToClipboardClickCommand: ICommand = new Command(fun () -> 
                                                                CrossShare.Current.SetClipboardText(clipboardText())
                                                                |> ignore ) :> ICommand

    let searchClickCommand: ICommand = new Command(fun () -> Debug.WriteLine("Search")) :> ICommand

    let mutable title: string = "Loading..."
    let mutable logoImageSource: ImageSource = null
    let mutable description: string = System.String.Empty
    let mutable searchButtonText: string = "Find more"
    let mutable wikipediaItems: IEnumerable<WikipediaItemViewModel> = Seq.empty

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

    //public event EventHandler<EventArgs> Initialized;

    //public Task InitAsync(PlatformItemViewModel targetPlatform)
    //{
    //    return Task.Factory.StartNew(async () =>
    //    {
    //        if (targetPlatform == null)
    //        {
    //            return;
    //        }
    //        var newGameName = _loaderService.GetRandomGame(targetPlatform.PlatformModel.FileName);
    //        _gameName = newGameName;
    //        _platformName = targetPlatform.Title;
    //        await InitGameAsync();
    //    });
    //}

    //public Task InitAsync(KeyValuePair<string, string> targetGame)
    //{
    //    return Task.Factory.StartNew(async () =>
    //    {
    //        _gameName = targetGame.Key;
    //        _platformName = targetGame.Value;
    //        await InitGameAsync();
    //    });
    //}

    //public Task InitListItemsAsync()
    //{
    //    return Task.Factory.StartNew(async () =>
    //    {
    //        WikipediaItems = await _wikiSearchService.GetItemsForQuery(_gameName);
    //    });
    //}

    //private Task InitGameAsync()
    //{
    //    return Task.Factory.StartNew(async () =>
    //    {

    //        var image = await _imageSearchService.GetImageForGame(_gameName, _platformName);
    //        Debug.WriteLine("The Flickr link is " + image);
    //        LogoImageSource = image;

    //        Title = _gameName;
    //        Description = string.Format("Your {0} game", _platformName);
    //        SearchButtonText = string.Format("Search \"{0} {1}\"", _platformName, _gameName);

    //        Initialized?.Invoke(this, EventArgs.Empty);
    //    });
    //}