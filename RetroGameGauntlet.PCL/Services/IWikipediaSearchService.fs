namespace RetroGameGauntlet.PCL.Services

open RetroGameGauntlet.PCL.Models
open RetroGameGauntlet.PCL.Models.API
open RetroGameGauntlet.PCL.ViewModels
open System.Threading.Tasks
open System.Collections.Generic

type IWikipediaSearchService = 
    abstract member GetWikipediaLinks: query:string -> Task<IEnumerable<WikipediaQuerySearchModel>>

    abstract member GetItemsForQuery: query:string -> Task<IEnumerable<WikipediaItemViewModel>>