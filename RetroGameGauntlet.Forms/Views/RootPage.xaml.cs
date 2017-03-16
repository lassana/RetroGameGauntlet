using Xamarin.Forms;

namespace RetroGameGauntlet.Forms.Views
{
    public partial class RootPage : TabbedPage
    {
        private int _selectedPageIndex;

        public RootPage()
        {
            InitializeComponent();

            //Children.Add(new RandomPage());
            Children.Add(new SearchPlatformsPage());
            Children.Add(new AboutPage());

            _selectedPageIndex = 0;
            CurrentPage = Children[_selectedPageIndex];

            CurrentPageChanged += (sender, e) =>
            {
                var previosPage = Children[_selectedPageIndex];
                (previosPage as ITabbedPageChild)?.Closed();
                (CurrentPage as ITabbedPageChild)?.Opened();
                _selectedPageIndex = Children.IndexOf(CurrentPage);
            };
        }
    }
}
