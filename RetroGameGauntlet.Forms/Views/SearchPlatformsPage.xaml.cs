using System.Collections.Generic;
using Xamarin.Forms;

namespace RetroGameGauntlet.Forms.Views
{
    public partial class SearchPlatformsPage : ContentPage
    {
        public SearchPlatformsPage()
        {
            InitializeComponent();
            if (Device.OS == TargetPlatform.iOS)
            {
                Icon = "ico_search.png";
            }
        }

        private void OnItemTap(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            Navigation.PushAsync(new OverviewPage { TargetGame = (KeyValuePair<string, string>) e.SelectedItem });
            ((ListView)sender).SelectedItem = null;
        }
    }
}
