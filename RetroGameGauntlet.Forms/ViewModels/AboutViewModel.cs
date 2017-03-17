using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using RetroGameGauntlet.Forms.Services;

namespace RetroGameGauntlet.Forms.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private ICommand _forkTapCommand;
        public ICommand ForkTapCommand
        {
            get
            {
                _forkTapCommand = _forkTapCommand ?? new Command((obj) => Device.OpenUri(new Uri("https://github.com/lassana/RetroGameGauntlet")));
                return _forkTapCommand;
            }
        }

        private ImageSource _randomImageSource;
        public ImageSource RandomImageSource
        {
            get { return _randomImageSource; }
            set 
            {
                _randomImageSource = value;
                RaisePropertyChanged(nameof(RandomImageSource));
            }
        }

        public string VersionName { get { return "1.0"; } }

        private readonly IImageSearchService _imageSearchService;

        public AboutViewModel(IImageSearchService imageSearchService)
        {
            _imageSearchService = imageSearchService;
        }

        public async Task InitAsync()
        {
            var newRandomImage = await _imageSearchService.GetImage();
            if (newRandomImage != (_randomImageSource as UriImageSource)?.Uri?.AbsoluteUri)
            {
                RandomImageSource = newRandomImage;
            }
        }
    }
}
