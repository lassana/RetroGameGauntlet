using Xamarin.Forms;

namespace RetroGameGauntlet.View
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

