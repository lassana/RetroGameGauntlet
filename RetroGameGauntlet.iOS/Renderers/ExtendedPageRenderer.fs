namespace RetroGameGauntlet.iOS.Renderers

open System
open UIKit
open Xamarin.Forms
open Xamarin.Forms.Platform.iOS

type ExtendedPageRenderer () as this =
    inherit PageRenderer ()

    let page() = this.Element :?> ContentPage

    override this.ViewWillAppear animated =
        base.ViewWillAppear(animated)

        //For some reasons it doesnt work with Xamarin.Forms 2.3
        //this.ViewController.ParentViewController.NavigationItem.BackBarButtonItem <- new UIBarButtonItem("<", UIBarButtonItemStyle.Plain, null)

        //So, default back button is being removed and a new one is put instead
        this.ViewController.ParentViewController.NavigationItem.SetHidesBackButton(true, false)

        let hdlr = EventHandler(fun sender args -> 
            async {
                let pg = page()
                pg.Navigation.PopAsync() |> Async.AwaitTask |> ignore
            } |> Async.StartImmediate)
        let backButton = new UIBarButtonItem("<", UIBarButtonItemStyle.Plain, hdlr)
        let fontName = Application.Current.Resources.Item "monoFont" :?> string
        let fontSize = nfloat 12.0
        let font = UIFont.FromName(fontName, fontSize)
        backButton.SetTitleTextAttributes(new UITextAttributes(Font=font), UIControlState.Normal)
        backButton.SetTitleTextAttributes(new UITextAttributes(Font=font), UIControlState.Selected)
        backButton.AccessibilityLabel <- "Back"

        this.ViewController.ParentViewController.NavigationItem.LeftBarButtonItem <- backButton

[<assembly: ExportRenderer(typeof<ContentPage>, typeof<ExtendedPageRenderer>)>] do ()