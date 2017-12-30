namespace RetroGameGauntlet.PCL.Services

open System.Collections.Generic
open System.Threading.Tasks

/// The platforms loader services.
type IPlatformLoaderService = 

    /// Gets the random game for a platform.
    abstract member GetRandomGameAsync: string -> Task<string>

    /// Gets the full list of games for a platform.
    abstract member FindGamesForAsync: string -> Task<IEnumerable<KeyValuePair<string, string>>>