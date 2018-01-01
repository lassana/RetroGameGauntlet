namespace RetroGameGauntlet.Core.Models

/// The game model (e.g. "Robocop" for NES platform).
type GameModel(game: string, platform: string) =
    member this.Game = game
    member this.Platform = platform