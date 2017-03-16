using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace RetroGameGauntlet.Forms.Models.Api
{
    public class WikipediaApiSearchResponseModel
    {
        [JsonProperty("batchcomplete")]
        public string BatchComplete { get; set; }

        [JsonProperty("continue")]
        public ContinueModel Continue { get; set; }

        [JsonProperty("query")]
        public QueryModel Query { get; set; }

        public class ContinueModel
        {
            [JsonProperty("sroffset")] //: 10,
            public string Sroffset { get; set; }

            [JsonProperty("continue")] //: "-||"
            public string Continue { get; set; }
        }

        public class QueryModel
        {
            [JsonProperty("searchinfo")]
            public SearchInfoModel SearchInfo { get; set; }

            [JsonProperty("search")]
            public List<SearchModel> Search { get; set; }

            public class SearchInfoModel 
            {
                [JsonProperty("totalhits")]
                public int TotalHits { get; set; }
            }

            public class SearchModel
            {
                [JsonProperty("ns")] //: 0,
                public int Ns { get; set; }
                [JsonProperty("title")] //: "Albert Einstein",
                public string Title { get; set; }
                [JsonProperty("snippet")] //: "&quot;<span class=\"searchmatch\">Einstein</span>&quot; redirects here. For other uses, see <span class=\"searchmatch\">Albert</span> <span class=\"searchmatch\">Einstein</span> (disambiguation) and <span class=\"searchmatch\">Einstein</span> (disambiguation). <span class=\"searchmatch\">Albert</span> <span class=\"searchmatch\">Einstein</span> (/ˈaɪnstaɪn/; German: [ˈalbɛɐ̯t",
                public string Snippet { get; set; }
                [JsonProperty("size")] //: 133559,
                public int Size { get; set; }
                [JsonProperty("wordcount")] //: 14401,
                public int WordCount { get; set; }
                [JsonProperty("timestamp")] //: "2016-05-02T10:13:53Z"
                public DateTime? Date { get; set; }
            }
        }

    }
}

