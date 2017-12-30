namespace RetroGameGauntlet.PCL.Views

open System.Diagnostics
open RetroGameGauntlet.PCL.ViewModels
open Xamarin.Forms
open Xamarin.Forms.Xaml

type RandomsPage() as this = 
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<RandomsPage>)

    do 
        if Device.RuntimePlatform = Device.iOS then
            this.Icon <- FileImageSource(File = "ico_application.png")
    
    member this.Handle_ItemTapped(sender : obj, e : SelectedItemChangedEventArgs) = 
        Debug.WriteLine "Handle_ItemTapped"
        if e.SelectedItem <> null then
            let targetPlatform = e.SelectedItem :?> PlatformItemViewModel
            (sender :?> ListView).SelectedItem <- null
            this.Navigation.PushAsync(new OverviewPage(targetPlatform)) |> ignore;