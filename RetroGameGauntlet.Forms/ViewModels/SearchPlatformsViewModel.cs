using System.Collections.Generic;
using System.Linq;
using RetroGameGauntlet.Forms.Services;
using Xamarin.Forms;

namespace RetroGameGauntlet.Forms.ViewModels
{
    public class SearchPlatformsViewModel : BaseViewModel
    {
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                RaisePropertyChanged(nameof(SearchText));
                Handle_SearchTextChanged();
            }
        }

        public bool HasItems
        {
            get { return Games?.Any() == true; }
        }

        private List<KeyValuePair<string, string>> _games;
        public List<KeyValuePair<string, string>> Games 
        { 
            get { return _games; }
            set
            {
                _games = value;
                RaisePropertyChanged(nameof(Games), nameof(HasItems));
            }
        }

        private readonly IPlatformLoaderService _platformLoader;

        public SearchPlatformsViewModel()
        {
            _platformLoader = DependencyService.Get<IPlatformLoaderService>();
        }

        private void Handle_SearchTextChanged()
        {
            Games = string.IsNullOrEmpty(SearchText)
                          ? null
                          : _platformLoader.FindGames(SearchText);
        }

   }
}
