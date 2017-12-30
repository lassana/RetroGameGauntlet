namespace RetroGameGauntlet.PCL.Services

open RetroGameGauntlet.PCL.Models.API
open RetroGameGauntlet.PCL.ViewModels
open System.Threading.Tasks
open System.Collections.Generic

/// The Wikipedia search service.
type IWikipediaSearchService = 

    /// Returns the list of Wikipedia queries.
    abstract member GetWikipediaLinksAsync: query:string -> Async<IEnumerable<WikipediaQuerySearchModel>>

    /// Returns the list of Wikipedia pages for query.
    abstract member GetItemsForQueryAsync: query:string -> Async<IEnumerable<WikipediaItemViewModel>>