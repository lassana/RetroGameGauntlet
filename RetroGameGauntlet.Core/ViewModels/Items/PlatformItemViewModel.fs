namespace RetroGameGauntlet.Core.ViewModels

open RetroGameGauntlet.Core.Models

/// The platform item view model.
type PlatformItemViewModel(platformModel: PlatformModel) = 
    member this.PlatformModel = platformModel
    member this.Title = platformModel.Title
    member this.Description = platformModel.Comment