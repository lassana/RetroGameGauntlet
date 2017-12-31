namespace RetroGameGauntlet.PCL.ViewModels

open System
open System.Collections.Generic
open RetroGameGauntlet.PCL.Services
open RetroGameGauntlet.PCL.ViewModels.Items

/// The "Search" page view model.
type SearchPlatformsViewModel(platformLoader: IPlatformLoaderService) =
    inherit ViewModelBase()
    let mutable games: seq<GameItemViewModel> = Seq.empty
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
                    this.Games <- newGames |> Seq.map(fun x -> GameItemViewModel(x))
                }
                |> Async.StartImmediate