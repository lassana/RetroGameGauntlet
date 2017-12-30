namespace RetroGameGauntlet.PCL.Services

open System.Collections.Generic
open System.Threading.Tasks

/// The platforms loader services.
type IPlatformLoaderService = 

    /// Gets the random game for a platform.
    abstract member GetRandomGameAsync: string -> Async<string>

    /// Gets the full list of games for a platform.
    abstract member FindGamesForAsync: string -> Async<IEnumerable<KeyValuePair<string, string>>>