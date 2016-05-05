﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using Model;
using System.Linq;
using System.Collections.Generic;

namespace Core
{
    public class WikipediaSearch
    {
        public WikipediaSearch()
        {
        }

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

