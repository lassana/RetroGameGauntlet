namespace RetroGameGauntlet.PCL.ViewModels

open System

type WikipediaItemViewModel(title: string, description: string) = 
    member this.Title = title
    member this.Description = description
    member this.Url = "https://en.wikipedia.org/wiki/" + this.Title
    member this.Uri = Uri(this.Url)