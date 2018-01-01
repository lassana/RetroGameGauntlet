namespace RetroGameGauntlet.Core.Models

/// The Wikipedia page.
type WikipediaPageModel(title: string, description: string) =
    member this.Title = title
    member this.Description = description