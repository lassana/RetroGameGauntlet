
using Xamarin.Forms;
using System;
using System.Threading.Tasks;
using Model;
using ViewModel;
using System.Diagnostics;
using Core;
using System.Collections.Generic;
using Plugin.Share;
using Plugin.Share.Abstractions;

namespace View.Overview
{
    public partial class OverviewPage : ContentPage
    {
        private double targetLayoutHeight;

        private string gameName;
        private string platformName;

        public PlatformViewModel TargetPlatform
        {
            set
            {
                LoadGameInfo(value);
            }
        }

        public KeyValuePair<string,string> TargetGame
        {
            set
            {
                InitGame(value.Key, value.Value);
            }
        }

        public OverviewPage()
        {
            InitializeComponent();

            SizeChanged += OnSizeChanged;

            descriptionLayout.BackgroundColor = Colors.BackgroundColor;
            descriptionCLayout.BackgroundColor = Colors.BackgroundColorLighter;

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

            var image = await new FlickImageSearch().GetImageForGame(gameName, platformName);
            Debug.WriteLine("The Flickr link is " + image);
            flickrImage.Source = image;

            Title = gameNameLabel.Text = gameName;
            descriptionATitle.Text = string.Format("Your {0} game", platformName);
            seachButton.Text = string.Format("Search \"{0} {1}\"", platformName, gameName);

            loadingLayout.IsVisible = false;
            descriptionLayout.IsVisible = true;
            AnimateDescriptionViews(descriptionALayout, null);

            descriptionBLayout.ItemsSource = await WikipediaLinkViewModel.ForQuery(gameName);
        }

        private async void LoadGameInfo(PlatformViewModel targetPlatform)
        {
            if (targetPlatform == null)
            {
                return;
            }
            var gameName = DependencyService.Get<IPlatformLoader>().GetRandomGame(targetPlatform.FileName);
            await InitGame(gameName, targetPlatform.Title);
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
            var wikiPage = e.SelectedItem as WikipediaLinkViewModel;
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
            rootLayout.Animate("descriptionAnimation", (arg) =>
                {
                    if (viewToShow != null)
                    {
                        viewToShow.HeightRequest = arg * targetLayoutHeight;
                        viewToShow.Opacity = arg;
                    }
                    if (viewToHide != null)
                    {
                        viewToHide.HeightRequest = (1 - arg) * targetLayoutHeight;
                        viewToHide.Opacity = (1 - arg);
                    }
                }, 16, 250, Easing.CubicInOut, (a, b) =>
                {
                    if (viewToShow != null)
                        viewToShow.HeightRequest = targetLayoutHeight;
                    //viewToShow.Opacity = 1;
                    if (viewToHide != null)
                        viewToHide.HeightRequest = 0;
                    //viewToShow.Opacity = 0;
                });
        }

        private Xamarin.Forms.View GetDisplayedView()
        {
            if (descriptionALayout.Height > 0)
            {
                return descriptionALayout;
            }
            else if (descriptionBLayout.Height > 0)
            {
                return descriptionBLayout;
            }
            else if (descriptionCLayout.Height > 0)
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

