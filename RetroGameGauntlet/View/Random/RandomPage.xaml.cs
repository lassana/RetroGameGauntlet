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
            if (e.SelectedItem == null) {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            //(e.SelectedItem as PlatformViewModel) //todo randomize
            Navigation.PushAsync(new OverviewPage());
            ((ListView)sender).SelectedItem = null; //uncomment 

        }
    }
}

