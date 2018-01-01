namespace RetroGameGauntlet.Core.ViewModels

open System
open RetroGameGauntlet.Core.Models

/// The Wikipedia link view model.
type WikipediaItemViewModel(model: WikipediaPageModel) = 
    member this.Title = model.Title
    member this.Description = model.Description
    member this.Url = "https://en.wikipedia.org/wiki/" + this.Title
    member this.Uri = Uri(this.Url)