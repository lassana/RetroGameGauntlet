using RetroGameGauntlet.Forms.ViewModels;
using Xamarin.Forms;

namespace RetroGameGauntlet.Forms.Views
{
    public partial class RandomPage : ContentPage
    {
        public RandomPage()
        {
            InitializeComponent();

            if (Device.OS == TargetPlatform.iOS)
            {
                Icon = "ico_application.png";
            }
        }

        private void OnItemTap(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            Navigation.PushAsync(new OverviewPage { TargetPlatform = (e.SelectedItem as PlatformItemViewModel) });
            ((ListView)sender).SelectedItem = null;
        }
    }
}
