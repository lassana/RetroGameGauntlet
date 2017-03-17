using System.Windows.Input;
using Plugin.Share;
using Xamarin.Forms;
using System.Threading.Tasks;
using RetroGameGauntlet.Forms.Services;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using Plugin.Share.Abstractions;

namespace RetroGameGauntlet.Forms.ViewModels
{
    public class OverviewViewModel : BaseViewModel
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            private set
            {
                _title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        public bool IsClipboardEnabled
        {
            get { return CrossShare.Current.SupportsClipboard; }
        }

        public ImageSource _logoImageSource;
        public ImageSource LogoImageSource
        {
            get { return _logoImageSource; }
            set
            {
                _logoImageSource = value;
                RaisePropertyChanged(nameof(LogoImageSource));
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        private string _searchButtonText;
        public string SearchButtonText
        {
            get { return _searchButtonText; }
            set
            {
                _searchButtonText = value;
                RaisePropertyChanged(nameof(SearchButtonText));
            }
        }

        #region `Click` properties

        private ICommand _shareClickCommand;
        public ICommand ShareClickCommand
        {
            get
            {
                _shareClickCommand = _shareClickCommand
                    ?? new Command(() => CrossShare.Current.Share(new ShareMessage
                    {
                        Text = string.Format("My {0} game is {1}", _platformName, _gameName),
                        Title = _gameName
                    }));
                return _shareClickCommand;
            }
        }

        private ICommand _copyToClipboardClickCommand;
        public ICommand CopyToClipboardClickCommand
        {
            get
            {
                _copyToClipboardClickCommand = _copyToClipboardClickCommand
                    ?? new Command(() => CrossShare.Current.SetClipboardText(string.Format("My {0} game is {1}", _platformName, _gameName)));
                return _copyToClipboardClickCommand;
            }
        }

        private ICommand _searchClickCommand;
        public ICommand SearchClickCommand
        {
            get
            {
                _searchClickCommand = _searchClickCommand ?? new Command(() => { });
                return _searchClickCommand;
            }
        }

        private IEnumerable<WikipediaItemViewModel> _wikipediaItems;
        public IEnumerable<WikipediaItemViewModel> WikipediaItems
        {
            get { return _wikipediaItems; }
            set
            {
                _wikipediaItems = value;
                RaisePropertyChanged(nameof(WikipediaItems));
            }
        }

        #endregion

        public event EventHandler<EventArgs> Initialized;

        private string _gameName;
        private string _platformName;

        private readonly IPlatformLoaderService _loaderService;
        private readonly IImageSearchService _imageSearchService;
        private readonly IWikipediaSearchService _wikiSearchService;

        public OverviewViewModel(IPlatformLoaderService loaderService,
                                 IImageSearchService imageSearchService,
                                 IWikipediaSearchService wikiSearchService)
        {
            _loaderService = loaderService;
            _imageSearchService = imageSearchService;
            _wikiSearchService = wikiSearchService;

            Title = "Loading...";
            SearchButtonText = "Find more";
        }

        public Task InitAsync(PlatformItemViewModel targetPlatform)
        {
            return Task.Factory.StartNew(async () =>
            {
                if (targetPlatform == null)
                {
                    return;
                }
                var newGameName = _loaderService.GetRandomGame(targetPlatform.PlatformModel.FileName);
                _gameName = newGameName;
                _platformName = targetPlatform.Title;
                await InitGameAsync();
            });
        }

        public Task InitAsync(KeyValuePair<string, string> targetGame)
        {
            return Task.Factory.StartNew(async () =>
            {
                _gameName = targetGame.Key;
                _platformName = targetGame.Value;
                await InitGameAsync();
            });
        }

        public Task InitListItemsAsync()
        {
            return Task.Factory.StartNew(async () =>
            {
                WikipediaItems = await _wikiSearchService.GetItemsForQuery(_gameName);
            });
        }

        private Task InitGameAsync()
        {
            return Task.Factory.StartNew(async () =>
            {

                var image = await _imageSearchService.GetImageForGame(_gameName, _platformName);
                Debug.WriteLine("The Flickr link is " + image);
                LogoImageSource = image;

                Title = _gameName;
                Description = string.Format("Your {0} game", _platformName);
                SearchButtonText = string.Format("Search \"{0} {1}\"", _platformName, _gameName);

                Initialized?.Invoke(this, EventArgs.Empty);
            });
        }
    }
}
