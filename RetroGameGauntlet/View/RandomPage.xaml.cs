using Xamarin.Forms;
using RetroGameGauntlet.ViewModel;

namespace RetroGameGauntlet.View
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

            listView.ItemsSource = PlatformViewModel.List;
        }


        private void OnItemTap(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            Navigation.PushAsync(new OverviewPage{ TargetPlatform = (e.SelectedItem as PlatformViewModel) });
            ((ListView)sender).SelectedItem = null;
        }
    }
}

