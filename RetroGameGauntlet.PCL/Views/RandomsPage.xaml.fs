namespace RetroGameGauntlet.PCL.Views

open RetroGameGauntlet.PCL.ViewModels
open Xamarin.Forms
open Xamarin.Forms.Xaml

type RandomsPage() as this = 
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<RandomsPage>)
    let iOS = Device.OS = TargetPlatform.iOS // Device.RuntimePlatform causes crash
    
    do 
        if iOS then this.Icon <- FileImageSource(File = "ico_application.png")
    
    member this.Handle_ItemTapped(sender : obj, e : SelectedItemChangedEventArgs) = 
        System.Diagnostics.Debug.WriteLine "Handle_ItemTapped"
        if e.SelectedItem <> null then
            let targetPlatform = e.SelectedItem :?> PlatformItemViewModel
            (sender :?> ListView).SelectedItem <- null
            this.Navigation.PushAsync(new OverviewPage(targetPlatform)) |> ignore;