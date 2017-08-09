namespace RetroGameGauntlet.PCL.ViewModels

open RetroGameGauntlet.PCL

[<AbstractClass; Sealed>]
type ViewModelLocator =

    static member AboutViewModel: AboutViewModel = 
        RetroGameGauntletCore.Container.GetInstance<AboutViewModel>()

    static member RandomsViewModel: RandomsViewModel = RandomsViewModel()

    static member SearchPlatformsViewModel: SearchPlatformsViewModel =
        RetroGameGauntletCore.Container.GetInstance<SearchPlatformsViewModel>()

    static member OverviewViewModel:OverviewViewModel =
        RetroGameGauntletCore.Container.GetInstance<OverviewViewModel>()