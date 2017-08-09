namespace RetroGameGauntlet.PCL.Views

open System.Collections.Generic
open Xamarin.Forms
open Xamarin.Forms.Xaml

type SearchPlatformsPage() as this = 
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<SearchPlatformsPage>)
    let iOS = Device.OS = TargetPlatform.iOS // Device.RuntimePlatform causes crash
    
    do 
        if iOS then this.Icon <- FileImageSource(File = "ico_search.png")

    member this.Handle_ItemTapped(sender : obj, e : SelectedItemChangedEventArgs) = 
        System.Diagnostics.Debug.WriteLine "Handle_ItemTapped"
        if e.SelectedItem <> null then
            let targetGame = e.SelectedItem :?> KeyValuePair<string, string>
            (sender :?> ListView).SelectedItem <- null
            this.Navigation.PushAsync(new OverviewPage(targetGame)) |> ignore