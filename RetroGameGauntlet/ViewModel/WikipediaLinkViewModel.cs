using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetroGameGauntlet.Core;
using System.Text.RegularExpressions;

namespace RetroGameGauntlet.ViewModel
{
    public class WikipediaLinkViewModel
    {
        public WikipediaLinkViewModel()
        {
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get { return "https://en.wikipedia.org/wiki/" + Title; } }

        public Uri Uri { get { return new Uri(Url); } }

        public static async Task<List<WikipediaLinkViewModel>> ForQuery(string query)
        {
            var wikipediaPages = await new WikipediaSearch().GetWikipediaLinks(query);

            if (wikipediaPages != null)
            {
                return wikipediaPages.Select((arg1, arg2) => new WikipediaLinkViewModel
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
            text = System.Net.WebUtility.HtmlDecode(text); 
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

