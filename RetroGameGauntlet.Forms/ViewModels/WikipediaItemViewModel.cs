using System;
namespace RetroGameGauntlet.Forms.ViewModels
{
    public class WikipediaItemViewModel : BaseViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get { return "https://en.wikipedia.org/wiki/" + Title; } }

        public Uri Uri { get { return new Uri(Url); } }
    }
}
