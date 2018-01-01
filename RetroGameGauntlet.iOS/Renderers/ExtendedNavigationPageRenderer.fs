namespace RetroGameGauntlet.iOS.Renderers

open System
open UIKit
open Xamarin.Forms
open Xamarin.Forms.Platform.iOS

type ExtendedNavigationPageRenderer () =
    inherit NavigationRenderer ()

    override this.ViewWillAppear animated =
        base.ViewWillAppear(animated)

        this.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default)
        this.NavigationBar.ShadowImage <- new UIImage()

        let fontName = Application.Current.Resources.Item "monoFont" :?> string
        let fontSize = nfloat 12.0
        let fontColor = ColorExtensions.ToUIColor (Application.Current.Resources.Item "textColor" :?> Color)
        let font = UIFont.FromName(fontName, fontSize)
        this.NavigationBar.TitleTextAttributes <- new UIStringAttributes(Font=font, ForegroundColor=fontColor)

[<assembly: ExportRenderer(typeof<NavigationPage>, typeof<ExtendedNavigationPageRenderer>)>] do ()