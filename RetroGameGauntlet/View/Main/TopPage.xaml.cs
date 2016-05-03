using System;
using System.Collections.Generic;

using Xamarin.Forms;
using View.Random;
using View.Search;
using View.About;
using ViewModel;

namespace View.Main
{
    public partial class TopPage : TabbedPage
    {
        private int selectedPageIndex;

        public TopPage()
        {
            InitializeComponent();

            Children.Add(new RandomPage());
            Children.Add(new SearchPlatformsPage());
            Children.Add(new AboutPage());

            selectedPageIndex = 0;
            CurrentPage = Children[selectedPageIndex];

            CurrentPageChanged += (sender, e) => 
            {
                var previosPage = Children[selectedPageIndex];
                (previosPage as ITabbedPageChild)?.Closed();
                (CurrentPage as ITabbedPageChild)?.Opened();
                selectedPageIndex = Children.IndexOf(CurrentPage);
            };
        }
    }
}

