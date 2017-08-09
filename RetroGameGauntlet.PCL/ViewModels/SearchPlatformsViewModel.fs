namespace RetroGameGauntlet.PCL.ViewModels

open System
open RetroGameGauntlet.PCL.Models
open RetroGameGauntlet.PCL.Services
open System.Collections.Generic

type SearchPlatformsViewModel(platformLoader: IPlatformLoaderService) =
    inherit ViewModelBase()
    let mutable games: seq<KeyValuePair<string, string>> = Seq.empty
    let mutable searchText: string = String.Empty

    member this.Games
        with get() = games
        and set parameter =
            games <- parameter
            this.NotifyPropertyChanged <@ this.Games @>
            this.NotifyPropertyChanged <@ this.HasItems @>

    member this.HasItems = not (this.Games |> Seq.isEmpty)

    member this.SearchText
        with get() = searchText
        and set parameter =
            searchText <- parameter
            this.NotifyPropertyChanged <@ this.SearchText @>
            if String.IsNullOrEmpty searchText then
                this.Games <- Seq.empty
            else 
                async {
                    let! newGames = platformLoader.FindGamesFor(searchText) |> Async.AwaitTask
                    this.Games <- newGames
                }
                |> Async.RunSynchronously