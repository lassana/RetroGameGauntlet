namespace RetroGameGauntlet.PCL.ViewModels

open System
open RetroGameGauntlet.PCL.Services
open System.Collections.Generic

/// The "Search" page view model.
type SearchPlatformsViewModel(platformLoader: IPlatformLoaderService) =
    inherit ViewModelBase()
    let mutable games: seq<KeyValuePair<string, string>> = Seq.empty
    let mutable searchText: string = String.Empty

    /// The listview source.
    member this.Games
        with get() = games
        and set parameter =
            games <- parameter
            this.NotifyPropertyChanged <@ this.Games @>
            this.NotifyPropertyChanged <@ this.HasItems @>

    /// A member describes if there is any game in the listview source.
    member this.HasItems = not (this.Games |> Seq.isEmpty)

    /// The serach filter.
    member this.SearchText
        with get() = searchText
        and set parameter =
            searchText <- parameter
            this.NotifyPropertyChanged <@ this.SearchText @>
            if String.IsNullOrEmpty searchText then
                this.Games <- Seq.empty
            else 
                async {
                    let! newGames = platformLoader.FindGamesForAsync(searchText)
                    this.Games <- newGames
                }
                |> Async.RunSynchronously