using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Model
{
    public class FlickApiSearchResponseModel
    {
        [JsonProperty("stat")]
        public string Stat { get; set; }

        [JsonProperty("photos")]
        public FlickPhotosModel Photos { get; set; }

        public class FlickPhotosModel
        {
            [JsonProperty("page")]//: 1,
            public int Page { get; set; }

            [JsonProperty("pages")]//: 84164,
            public int Pages { get; set; }

            [JsonProperty("perpage")]//: 1,
            public int PerPage { get; set; }

            [JsonProperty("total")]//: "84164",
            public string Total { get; set; }

            [JsonProperty("photo")]
            public List<FlickPhotoModel> Photo { get; set; }
        }

        public class FlickPhotoModel
        {
            [JsonProperty("id")] //: "26743945911",
            public string Id { get; set; }

            [JsonProperty("owner")] //: "126053939@N08",
            public string Owner { get; set; }

            [JsonProperty("secret")] //: "6b1f23f541",
            public string Secret { get; set; }

            [JsonProperty("server")] //: "7468",
            public string Server { get; set; }

            [JsonProperty("farm")] //: 8,
            public int Farm { get; set; }

            [JsonProperty("title")] //: "Space Harrier 2 (Mega Drive)",
            public string Title { get; set; }

            [JsonProperty("ispublic")] //: 1,
            public int IsPublic { get; set; }

            [JsonProperty("isfriend")] //: 0,
            public int IsFriend { get; set; }

            [JsonProperty("isfamily")] //: 0
            public int IsFamily { get; set; }
        }
    }
}

