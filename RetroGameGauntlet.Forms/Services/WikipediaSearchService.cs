using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;
using RetroGameGauntlet.Forms.Models.Api;

namespace RetroGameGauntlet.Forms.Services
{
    public class WikipediaSearchService
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
    }
}

