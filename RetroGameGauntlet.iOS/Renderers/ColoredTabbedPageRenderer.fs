namespace RetroGameGauntlet.iOS.Renderers

open System
open UIKit
open Xamarin.Forms
open Xamarin.Forms.Platform.iOS

type ColoredTabbedPageRenderer () as this =
    inherit TabbedRenderer ()

    do
        let backgroundColor = Application.Current.Resources.Item "backgroundColor" :?> Color
        let textColor = Application.Current.Resources.Item "textColor" :?> Color

        this.TabBar.TintColor <- ColorExtensions.ToUIColor textColor
        this.TabBar.BarTintColor <- ColorExtensions.ToUIColor backgroundColor
        this.TabBar.BackgroundColor <- UIColor.Blue

        let fontName = Application.Current.Resources.Item "monoFont" :?> string
        let fontSize = nfloat 8.0
        let font = UIFont.FromName(fontName, fontSize)
        let textAttributes = new UITextAttributes(Font=font)
        UITabBarItem.Appearance.SetTitleTextAttributes(textAttributes, UIControlState.Normal)

[<assembly: ExportRenderer(typeof<TabbedPage>, typeof<ColoredTabbedPageRenderer>)>] do ()