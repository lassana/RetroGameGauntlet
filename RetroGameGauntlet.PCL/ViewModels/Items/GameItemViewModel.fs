namespace RetroGameGauntlet.PCL.ViewModels.Items

open RetroGameGauntlet.PCL.Models

/// The game list item view model.
type GameItemViewModel(game: GameModel) = 
    member this.Title = game.Game
    member this.Game = game.Game
    member this.Description = game.Platform
    member this.Platform = game.Platform