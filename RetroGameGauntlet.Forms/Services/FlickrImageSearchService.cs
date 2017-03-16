using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Diagnostics;
using RetroGameGauntlet.Forms.Models.Api;

namespace RetroGameGauntlet.Forms.Services
{
    public class FlickrImageSearchService
    {
        private const string ApiKey = "78d991c72ad7a124dcef3c2796d6f4ea";

        private async Task<string> GetImageFromLink(string link)
        {
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
            var responseObject = JsonConvert.DeserializeObject<FlickApiSearchResponseModel>(responseBody);

            if (responseObject?.Photos?.Photo?.Any() == true)
            {
                var photo = responseObject?.Photos?.Photo?.First();
                if (photo != null)
                {
                    return "https://" + "farm" + photo.Farm + ".staticflickr.com/" + photo.Server + "/" + photo.Id + "_" + photo.Secret + "_z" + ".jpg";
                }
            }
            return null;
        }

        public async Task<string> GetImage()
        {
            return await GetImageFromLink(GetLinkForQuery("retro game"));
        }

        public async Task<string> GetImageForGame(string gameName, string platformName)
        {
            var img = await GetImageFromLink(GetLinkForQuery(gameName + " " + platformName));
            return img ?? await GetImageFromLink(GetLinkForQuery(platformName));
        } 

        private string GetLinkForQuery(string query) 
        {
            return "https://api.flickr.com/services/rest/"
                            + "?format=" + "json"
                            + "&nojsoncallback=" + "1"
                            + "&method=" + "flickr.photos.search"
                            + "&api_key=" + ApiKey
                            + "&per_page=" + "1"
                            + "&text=" + query;
        }
    }
}

