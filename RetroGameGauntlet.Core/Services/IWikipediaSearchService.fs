namespace RetroGameGauntlet.Core.Services

open RetroGameGauntlet.Core.Models
open RetroGameGauntlet.Core.Models.API
open System.Threading.Tasks
open System.Collections.Generic

/// The Wikipedia search service.
type IWikipediaSearchService = 

    /// Returns the list of Wikipedia queries.
    abstract member GetWikipediaLinksAsync: query:string -> Async<IEnumerable<WikipediaQuerySearchModel>>

    /// Returns the list of Wikipedia pages for query.
    abstract member GetItemsForQueryAsync: query:string -> Async<IEnumerable<WikipediaPageModel>>