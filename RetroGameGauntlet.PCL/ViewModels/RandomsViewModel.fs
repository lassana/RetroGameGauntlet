namespace RetroGameGauntlet.PCL.ViewModels

open RetroGameGauntlet.PCL.Models
open System.Collections.Generic

/// The "Random" page view model.
type RandomsViewModel() =
    inherit ViewModelBase()

    let platforms = Platforms.All 
                    |> Seq.map (fun arg -> PlatformItemViewModel(arg))
    
    /// The "all paltforms" listview source.
    member this.Platforms: IEnumerable<PlatformItemViewModel> = platforms