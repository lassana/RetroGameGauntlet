namespace RetroGameGauntlet.iOS.Renderers

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

[<assembly: ExportRenderer(typeof<TabbedPage>, typeof<ColoredTabbedPageRenderer>)>] do ()