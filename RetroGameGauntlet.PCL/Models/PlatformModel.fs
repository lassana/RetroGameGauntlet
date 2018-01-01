namespace RetroGameGauntlet.PCL.Models

/// The game platfrom (e.g. NES).
type PlatformModel(title: string, comment: string, fileName: string) =
    member this.Title = title
    member this.Comment = comment
    member this.FileName = fileName