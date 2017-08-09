namespace RetroGameGauntlet.PCL.ViewModels

open RetroGameGauntlet.PCL.Models
open System.Collections.Generic

type RandomsViewModel() =
    inherit ViewModelBase()

    let platforms = Platforms.All 
                    |> Seq.map (fun arg -> PlatformItemViewModel(arg))

    member this.Platforms: IEnumerable<PlatformItemViewModel> = platforms