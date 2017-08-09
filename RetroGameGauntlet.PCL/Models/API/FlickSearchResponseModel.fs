namespace RetroGameGauntlet.PCL.Models.API

open Newtonsoft.Json
open System
open System.Collections.Generic

type FlickPhotoModel = {
    [<JsonProperty("id")>] //: "26743945911",
    Id: string

    [<JsonProperty("owner")>] //: "126053939@N08",
    Owner: string

    [<JsonProperty("secret")>] //: "6b1f23f541",
    Secret: string

    [<JsonProperty("server")>] //: "7468",
    Server: string

    [<JsonProperty("farm")>] //: 8,
    Farm: int

    [<JsonProperty("title")>] //: "Space Harrier 2 (Mega Drive)",
    Title: string

    [<JsonProperty("is")>] //: 1,
    Is: int

    [<JsonProperty("isfriend")>] //: 0,
    IsFriend: int

    [<JsonProperty("isfamily")>] //: 0
    IsFamily: int
}

 type FlickPhotosModel = {
    [<JsonProperty("page")>]//: 1,
    Page: int

    [<JsonProperty("pages")>]//: 84164,
    Pages: int

    [<JsonProperty("perpage")>]//: 1,
    PerPage: int 

    [<JsonProperty("total")>]//: "84164",
    Total: string

    [<JsonProperty("photo")>]
    Photo: IEnumerable<FlickPhotoModel>
}

type FlickSearchResponseModel = {
    [<JsonProperty("stat")>]
    Stat: string

    [<JsonProperty("photos")>]
    Photos: FlickPhotosModel
}