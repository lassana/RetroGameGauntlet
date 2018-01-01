namespace RetroGameGauntlet.PCL.ViewModels

open RetroGameGauntlet.PCL.Models

/// The platform item view model.
type PlatformItemViewModel(platformModel: PlatformModel) = 
    member this.PlatformModel = platformModel
    member this.Title = platformModel.Title
    member this.Description = platformModel.Comment