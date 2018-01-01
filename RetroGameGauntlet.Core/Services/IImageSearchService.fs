namespace RetroGameGauntlet.Core.Services

open System.Threading.Tasks

/// The image search service.
type IImageSearchService = 

    /// Returns the random image URL.
    abstract member GetImageAsync: Async<string>

    /// Returns the random image URL for a game name.
    abstract member GetImageForGameAsync: gameName:string -> platformName:string -> Async<string>