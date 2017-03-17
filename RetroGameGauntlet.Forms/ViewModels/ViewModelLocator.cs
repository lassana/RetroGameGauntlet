namespace RetroGameGauntlet.Forms.ViewModels
{
    public static class ViewModelLocator
    {
        public static AboutViewModel AboutViewModel => RetroGameGauntletApp.Container.GetInstance<AboutViewModel>();

        public static SearchPlatformsViewModel SearchPlatformsViewModel => RetroGameGauntletApp.Container.GetInstance<SearchPlatformsViewModel>();

        public static RandomViewModel RandomViewModel => new RandomViewModel();

        public static OverviewViewModel OverviewViewModel => RetroGameGauntletApp.Container.GetInstance<OverviewViewModel>();
    }
}
