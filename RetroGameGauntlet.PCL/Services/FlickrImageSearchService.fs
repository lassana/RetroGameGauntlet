namespace RetroGameGauntlet.PCL.Services

open Newtonsoft.Json
open RetroGameGauntlet.PCL
open RetroGameGauntlet.PCL.Models.API
open RetroGameGauntlet.PCL.Services
open System.Diagnostics
open System.Linq
open System.Net.Http
open System.Net

type FlickrImageSearchService() = 
    let apiKey: string = "78d991c72ad7a124dcef3c2796d6f4ea";

    let getLinkForQuery query:string =
        "https://api.flickr.com/services/rest/" 
        + "?format=" + "json"
        + "&nojsoncallback=" + "1"
        + "&method=" + "flickr.photos.search"
        + "&api_key=" + apiKey
        + "&per_page=" + "1"
        + "&text=" + query

    interface IImageSearchService with
        member this.GetImage =
            async {
                let link = this.GetLinkForQuery("retro game")
                let! a = this.GetImageFromLink(link)
                return if a.IsSome then a.Value else System.String.Empty
            }
            |> Async.StartAsTask

        member this.GetImageForGame gameName platformName = 
            async {
                let! img = this.GetImageFromLink(this.GetLinkForQuery(gameName + " " + platformName))
                return if img.IsSome then img.Value else System.String.Empty
            }
            |> Async.StartAsTask


    member private this.GetImageFromLink (link: string) : Async<Option<string>> = 
        async {
            try
                Debug.WriteLine("GetImageFromLink " + link)
                let! response = RetroGameGauntletCore.HttpClient.GetAsync(link) |> Async.AwaitTask
                let! responseBody = response.Content.ReadAsStringAsync() |> Async.AwaitTask
                Debug.WriteLine(System.String.Format("Request URI is {0}, response JSON is {1}", link, responseBody))
                let responseObject = JsonConvert.DeserializeObject<FlickSearchResponseModel>(responseBody)
                if responseObject.Photos.Photo.Any() then
                    let photo = responseObject.Photos.Photo.First()
                    let rvalue = System.String.Format("https://farm{0}.staticflickr.com/{1}/{2}_{3}_z.jpg",
                                                      photo.Farm,
                                                      photo.Server,
                                                      photo.Id,
                                                      photo.Secret)
                    return Some rvalue
                else
                    return None
            with
            | e ->
                Debug.WriteLine("Error in GetImageFromLink" + e.ToString())
                return None
        }

    member private this.GetLinkForQuery (query: string) =
        "https://api.flickr.com/services/rest/"
                        + "?format=" + "json"
                        + "&nojsoncallback=" + "1"
                        + "&method=" + "flickr.photos.search"
                        + "&api_key=" + apiKey
                        + "&per_page=" + "1"
                        + "&text=" + query;
