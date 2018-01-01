namespace RetroGameGauntlet.PCL.Views

open Xamarin.Forms
open Xamarin.Forms.Xaml

type CustomViewCell() =
    inherit ViewCell()
    let _ = base.LoadFromXaml(typeof<CustomViewCell>)
