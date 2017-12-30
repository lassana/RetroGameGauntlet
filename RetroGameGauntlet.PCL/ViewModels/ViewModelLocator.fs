namespace RetroGameGauntlet.PCL.ViewModels

open RetroGameGauntlet.PCL

/// The view model localtor. It can be used as a factory of view models.
[<AbstractClass; Sealed>]
type ViewModelLocator =

    static member AboutViewModel: AboutViewModel = 
        GauntletCore.Container.GetInstance<AboutViewModel>()

    static member RandomsViewModel: RandomsViewModel = RandomsViewModel()

    static member SearchPlatformsViewModel: SearchPlatformsViewModel =
        GauntletCore.Container.GetInstance<SearchPlatformsViewModel>()

    static member OverviewViewModel:OverviewViewModel =
        GauntletCore.Container.GetInstance<OverviewViewModel>()