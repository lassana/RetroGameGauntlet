namespace RetroGameGauntlet.Core.Services

open System.Collections.Generic
open System.Threading.Tasks
open RetroGameGauntlet.Core.Models

/// The platforms loader services.
type IPlatformLoaderService = 

    /// Gets the random game for a platform.
    abstract member GetRandomGameAsync: string -> Async<string>

    /// Gets the full list of games for a platform.
    abstract member FindGamesForAsync: string -> Async<IEnumerable<GameModel>>