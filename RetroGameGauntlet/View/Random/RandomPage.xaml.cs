using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ViewModel;
using View.Overview;

namespace View.Random
{
    public partial class RandomPage : ContentPage
    {
        public RandomPage()
        {
            InitializeComponent();

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

