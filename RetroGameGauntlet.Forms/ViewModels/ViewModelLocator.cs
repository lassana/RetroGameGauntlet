namespace RetroGameGauntlet.Forms.ViewModels
{
    public static class ViewModelLocator
    {
        public static AboutViewModel AboutViewModel => new AboutViewModel();

        public static SearchPlatformsViewModel SearchPlatformsViewModel => new SearchPlatformsViewModel();

        public static RandomViewModel RandomViewModel => new RandomViewModel();

        public static OverviewViewModel OverviewViewModel => new OverviewViewModel();
    }
}
