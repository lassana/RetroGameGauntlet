using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;
using RetroGameGauntlet.Forms.Models.Api;
using RetroGameGauntlet.Forms.ViewModels;
using System.Linq;
using System.Text.RegularExpressions;
using System;

namespace RetroGameGauntlet.Forms.Services
{
    public class WikipediaSearchService : IWikipediaSearchService
    {
        public async Task<List<WikipediaApiSearchResponseModel.QueryModel.SearchModel>> GetWikipediaLinks(string query) 
        {
            var link = GetSearchUrl(query);
            HttpResponseMessage response;
            try 
            {
                response = await new HttpClient().GetAsync(link);
            }
            catch (TaskCanceledException)
            {
                return null;
            }
            catch (WebException)
            {
                return null;
            }
            if (response.StatusCode != HttpStatusCode.OK) 
            {
                return null;
            }
            var responseBody = response.Content.ReadAsStringAsync().Result;
            Debug.WriteLine(string.Format("Request URL is {0}, response JSON is {1}", link, responseBody));
            var responseObject = JsonConvert.DeserializeObject<WikipediaApiSearchResponseModel>(responseBody);

            return responseObject?.Query?.Search;
        }

        private string GetSearchUrl(string query)
        {
            return "https://en.wikipedia.org/w/api.php"
                + "?action=query"
                + "&list=search"
                + "&format=json"
                + "&srsearch=" + query
                + "&utf8=";
        }

        public async Task<List<WikipediaItemViewModel>> GetItemsForQuery(string query)
        {
            var wikipediaPages = await GetWikipediaLinks(query);

            if (wikipediaPages != null)
            {
                return wikipediaPages.Select((arg1, arg2) => new WikipediaItemViewModel
                {
                    Title = arg1.Title,
                    Description = HtmlToPlainText(arg1.Snippet)
                }).ToList();
            }
            else
            {
                return null;
            }
        }

        private static string HtmlToPlainText(string html)
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            //Decode html specific characters
            text = WebUtility.HtmlDecode(text);
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }
    }
}

