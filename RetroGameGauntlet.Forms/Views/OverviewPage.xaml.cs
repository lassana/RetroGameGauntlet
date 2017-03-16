using System;
using System.Collections.Generic;
using RetroGameGauntlet.Forms.ViewModels;
using Xamarin.Forms;
using RetroGameGauntlet.Forms.Services;
using Plugin.Share;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RetroGameGauntlet.Forms.Views
{
    //TODO rewrite it using MVVM
    public partial class OverviewPage : ContentPage
    {
        private double targetLayoutHeight;

        private string gameName;
        private string platformName;

        public PlatformItemViewModel TargetPlatform
        {
            set
            {
                LoadGameInfo(value);
            }
        }

        public KeyValuePair<string, string> TargetGame
        {
            set
            {
                InitGame(value.Key, value.Value);
            }
        }

        public OverviewPage()
        {
            InitializeComponent();

            if (Device.OS == Xamarin.Forms.TargetPlatform.Android)
            {
                descriptionBLayout.BackgroundColor = Color.White;
            }

            SizeChanged += OnSizeChanged;

            descriptionLayout.BackgroundColor = (Color) Application.Current.Resources["backgroundColor"];
            descriptionCLayout.BackgroundColor = (Color)Application.Current.Resources["backgroundColorLighter"];

            loadingLayout.IsVisible = true;
            descriptionLayout.IsVisible = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            clipboardButton.IsEnabled = CrossShare.Current.SupportsClipboard;
        }

        public async Task InitGame(string gameName, string platformName)
        {
            this.gameName = gameName;
            this.platformName = platformName;

            await Task.Delay(250 * 2);

            var image = await new FlickrImageSearchService().GetImageForGame(gameName, platformName);
            Debug.WriteLine("The Flickr link is " + image);
            flickrImage.Source = image;

            Title = gameNameLabel.Text = gameName;
            descriptionATitle.Text = string.Format("Your {0} game", platformName);
            seachButton.Text = string.Format("Search \"{0} {1}\"", platformName, gameName);

            loadingLayout.IsVisible = false;
            descriptionLayout.IsVisible = true;
            AnimateDescriptionViews(descriptionALayout, null);

            descriptionBLayout.ItemsSource = await new WikipediaSearchService().GetItemsForQuery(gameName);
        }

        private async void LoadGameInfo(PlatformItemViewModel targetPlatform)
        {
            if (targetPlatform == null)
            {
                return;
            }
            var newGameName = DependencyService.Get<IPlatformLoaderService>().GetRandomGame(targetPlatform.PlatformModel.FileName);
            await InitGame(newGameName, targetPlatform.Title);
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            AbsoluteLayout.SetLayoutBounds(titleLayout, new Rectangle(0.5, 1, Width, 50));

            targetLayoutHeight = Height - 50 * 3;

            var displayedView = GetDisplayedView();
            if (displayedView != null)
                displayedView.HeightRequest = targetLayoutHeight;
        }

        private void OnWikipediaItemTap(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            var wikiPage = e.SelectedItem as WikipediaItemViewModel;
            //CrossShare.Current.OpenBrowser(wikiPage.Url, new BrowserOptions { UseSafairReaderMode = true });
            Device.OpenUri(wikiPage.Uri);
            ((ListView)sender).SelectedItem = null;
        }

        private void OnTitleClicked(object sender, EventArgs e)
        {
            var targetView = GetViewByTitleView(sender as Xamarin.Forms.View);
            var displayedView = GetDisplayedView();
            if (targetView != displayedView)
            {
                AnimateDescriptionViews(targetView, displayedView);
            }
        }

        private void AnimateDescriptionViews(Xamarin.Forms.View viewToShow, Xamarin.Forms.View viewToHide)
        {
            if (viewToShow != null)
            {
                viewToShow.Opacity = 1;
            }
            if (viewToHide != null)
            {
                viewToHide.Opacity = 1;
            }
            rootLayout.Animate("descriptionAnimation", (arg) =>
                {
                    if (viewToShow != null)
                    {
                        viewToShow.HeightRequest = Math.Round(arg * targetLayoutHeight);
                        viewToShow.Opacity = arg;
                    }
                    if (viewToHide != null)
                    {
                        viewToHide.HeightRequest = Math.Round((1 - arg) * targetLayoutHeight);
                        viewToHide.Opacity = (1 - arg);
                    }
                }, 16, 250, Easing.CubicInOut, (a, b) =>
                {
                    if (viewToShow != null)
                    {
                        viewToShow.HeightRequest = targetLayoutHeight;
                        viewToShow.Opacity = 1;
                    }
                    if (viewToHide != null)
                    {
                        viewToHide.HeightRequest = 0;
                        viewToHide.Opacity = 0;
                    }
                    //(viewToShow as Layout)?.ForceLayout();
                    //(viewToHide as Layout)?.ForceLayout();
                });
        }

        private Xamarin.Forms.View GetDisplayedView()
        {
            // Sometimes after calling "view.HeightRequest = 0" value of "view.Height" may be ~0.33
            // perhaps it's a droid-specific bug
            const int hiddenHeight = 1;
            const float hiddenOpacity = 0.1f;
            if (descriptionALayout.Height > hiddenHeight && descriptionALayout.Opacity > hiddenOpacity)
            {
                return descriptionALayout;
            }
            else if (descriptionBLayout.Height > hiddenHeight && descriptionBLayout.Opacity > hiddenOpacity)
            {
                return descriptionBLayout;
            }
            else if (descriptionCLayout.Height > hiddenHeight && descriptionCLayout.Opacity > hiddenOpacity)
            {
                return descriptionCLayout;
            }
            else
            {
                return null;
            }
        }

        private Xamarin.Forms.View GetViewByTitleView(Xamarin.Forms.View titleView)
        {
            if (titleView == descriptionATitle)
            {
                return descriptionALayout;
            }
            else if (titleView == descriptionBTitle)
            {
                return descriptionBLayout;
            }
            else if (titleView == descriptionCTitle)
            {
                return descriptionCLayout;
            }
            else
            {
                return null;
            }
        }

        private void OnShareClicked(object sender, EventArgs args)
        {
            CrossShare.Current.Share(string.Format("My {0} game is {1}", platformName, gameName), gameName);
        }

        private void OnCopyToClipboardClicked(object sender, EventArgs args)
        {
            CrossShare.Current.SetClipboardText(string.Format("My {0} game is {1}", platformName, gameName));
        }

        private void OnSearchClicked(object sender, EventArgs args)
        {

        }
    }
}
