namespace RetroGameGauntlet.PCL.Services

open Newtonsoft.Json
open RetroGameGauntlet.PCL
open RetroGameGauntlet.PCL.Models.API
open RetroGameGauntlet.PCL.Services
open RetroGameGauntlet.PCL.ViewModels
open System
open System.Diagnostics
open System.Net.Http
open System.Net
open System.Text.RegularExpressions

/// The Wikipedia search service.
type WikipediaSearchService() = 

    interface IWikipediaSearchService with

        /// Returns the list of Wikipedia queries.
        member this.GetWikipediaLinksAsync (query: string) =
            async {
                System.Threading.Tasks.Task.Delay(5000) |> Async.AwaitTask |> ignore
                let link = this.GetSearchUrl(query);
                try
                    let! response = GauntletCore.HttpClient.GetAsync(link) |> Async.AwaitTask
                    if response.StatusCode = HttpStatusCode.OK then
                        let! responseBody = response.Content.ReadAsStringAsync() |> Async.AwaitTask
                        Debug.WriteLine(System.String.Format("Request URI is {0}, response JSON is {1}", link, responseBody))
                        let responseObject = JsonConvert.DeserializeObject<WikipediaSearchResponseModel>(responseBody);
                        return responseObject.Query.Search
                    else
                        return Seq.empty<WikipediaQuerySearchModel>
                with
                | e ->
                    Debug.WriteLine("Error in GetWikipediaLinks: " + e.ToString())
                    return Seq.empty<WikipediaQuerySearchModel>
            }

        /// Returns the list of Wikipedia pages for query.
        member this.GetItemsForQueryAsync (squery: string) =
            async {
                System.Threading.Tasks.Task.Delay(5000) |> Async.AwaitTask |> ignore
                let! wikiPage = (this :> IWikipediaSearchService).GetWikipediaLinksAsync(squery)
                if wikiPage |> Seq.isEmpty then
                    return Seq.empty<WikipediaItemViewModel>
                else
                    let rvalue = wikiPage |> Seq.map(fun a -> 
                        WikipediaItemViewModel(a.Title, this.HtmlToPlainText(a.Snippet)))
                    return rvalue
            }

    member private this.GetSearchUrl query: string =
        "https://en.wikipedia.org/w/api.php"
                        + "?action=query"
                        + "&list=search"
                        + "&format=json"
                        + "&srsearch=" + query
                        + "&utf8=";

    member private this.HtmlToPlainText html: string =
        let tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";   // matches one or more (white space or line breaks) between '>' and '<'
        let stripFormatting = @"<[^>]*(>|$)";       // match any character between '<' and '>', even when end tag is missing
        let lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>"; // matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
        let lineBreakRegex = Regex(lineBreak, RegexOptions.Multiline);
        let stripFormattingRegex = Regex(stripFormatting, RegexOptions.Multiline);
        let tagWhiteSpaceRegex = Regex(tagWhiteSpace, RegexOptions.Multiline);

        let mutable text = html;
        // Decode html specific characters
        text <- WebUtility.HtmlDecode(text);
        // Remove tag whitespace/line breaks
        text <- tagWhiteSpaceRegex.Replace(text, "><");
        // Replace <br /> with line breaks
        text <- lineBreakRegex.Replace(input=text, replacement=Environment.NewLine)
        //Strip formatting
        text <- stripFormattingRegex.Replace(text, System.String.Empty);
        text