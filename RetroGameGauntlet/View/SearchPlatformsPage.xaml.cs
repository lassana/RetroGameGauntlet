using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using RetroGameGauntlet.Model;

namespace RetroGameGauntlet.View
{
    public partial class SearchPlatformsPage : ContentPage
    {
        private IPlatformLoader platformLoader = DependencyService.Get<IPlatformLoader>();

        public SearchPlatformsPage()
        {
            InitializeComponent();
            if(Device.OS == TargetPlatform.iOS)
            {
                Icon = "ico_search.png";
            }
        }

        private void OnSearchRequested(object sender, TextChangedEventArgs args)
        {
            if (string.IsNullOrEmpty(args.NewTextValue))
            {
                listView.ItemsSource = null;
                listView.IsVisible = false;
                notFoundLabel.IsVisible = true;
            }
            else
            {
                var games = platformLoader.FindGames(args.NewTextValue);
                listView.ItemsSource = games;
                listView.IsVisible = games.Any();
                notFoundLabel.IsVisible = games.Count == 0;
            }
        }

        private void OnItemTap(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            Navigation.PushAsync(new OverviewPage{ TargetGame = (KeyValuePair<string,string>)e.SelectedItem });
            ((ListView)sender).SelectedItem = null;
        }
    }
}

