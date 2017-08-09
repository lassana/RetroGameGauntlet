namespace RetroGameGauntlet.PCL.Models

type PlatformModel(title: string, comment: string, fileName: string) =
    member this.Title = title
    member this.Comment = comment
    member this.FileName = fileName