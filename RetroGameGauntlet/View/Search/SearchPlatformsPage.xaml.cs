using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ViewModel;

namespace View.Search
{
    public partial class SearchPlatformsPage : ContentPage
    {
        public SearchPlatformsPage()
        {
            InitializeComponent();

            listView.ItemsSource = PlatformViewModel.List;
        }
    }
}

