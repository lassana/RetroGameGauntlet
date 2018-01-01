namespace RetroGameGauntlet.Core.Models.API

open Newtonsoft.Json
open System
open System.Collections.Generic

type WikipediaSearchContinueModel = {
    [<JsonProperty("sroffset")>] //: 10,
    Sroffset: string

    [<JsonProperty("continue")>] //: "-||"
    Continue: string 
}

type WikipediaQuerySearchInfoModel = {
    [<JsonProperty("totalhits")>]
    TotalHits: int
}

/// The Wikipedia query search response.
type WikipediaQuerySearchModel = {
    [<JsonProperty("ns")>] //: 0,
    Ns: int

    [<JsonProperty("title")>] //: "Albert Einstein",
    Title: string

    [<JsonProperty("snippet")>] //: "&quot;<span class=\"searchmatch\">Einstein</span>&quot; redirects here. For other uses, see <span class=\"searchmatch\">Albert</span> <span class=\"searchmatch\">Einstein</span> (disambiguation) and <span class=\"searchmatch\">Einstein</span> (disambiguation). <span class=\"searchmatch\">Albert</span> <span class=\"searchmatch\">Einstein</span> (/ˈaɪnstaɪn/; German: [ˈalbɛɐ̯t",
    Snippet: string 

    [<JsonProperty("size")>] //: 133559,
    Size: int 

    [<JsonProperty("wordcount")>] //: 14401,
    WordCount: int

    [<JsonProperty("timestamp")>] //: "2016-05-02T10:13:53Z"
    Date: Nullable<DateTime>
}

type WikipediaQueryModel = {
    [<JsonProperty("searchinfo")>]
    SearchInfo: WikipediaQuerySearchInfoModel

    [<JsonProperty("search")>]
    Search: IEnumerable<WikipediaQuerySearchModel> 
}

/// The Wikipedia response.
type WikipediaSearchResponseModel = {

    [<JsonProperty("batchcomplete")>]
    BatchComplete: string

    [<JsonProperty("continue")>]
    Continue: WikipediaSearchContinueModel

    [<JsonProperty("query")>]
    Query: WikipediaQueryModel
}

