namespace RetroGameGauntlet.Core.Views

open Xamarin.Forms
open Xamarin.Forms.Xaml

type RootPage() = 
    inherit TabbedPage()
    let _ = base.LoadFromXaml(typeof<RootPage>)
