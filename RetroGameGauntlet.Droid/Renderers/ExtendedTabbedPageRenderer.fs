namespace RetroGameGauntlet.Droid.Renderers

open System
open Android.Content
open Android.Support.Design.Widget
open Android.Support.V7.Widget
open Android.Views
open RetroGameGauntlet.Droid
open Xamarin.Forms
open Xamarin.Forms.Platform.Android
open Xamarin.Forms.Platform.Android.AppCompat

/// The TabbedPage renderer that replaces the text style of an each tab tilte.
type ExtendedTabbedPageRenderer (context: Context) =
    inherit TabbedPageRenderer (context)

    override this.OnVisibilityChanged(changedView: Android.Views.View, visibility: ViewStates) =
        base.OnVisibilityChanged(changedView, visibility)
        if visibility = ViewStates.Visible then

            let fontName = Application.Current.Resources.Item "monoFont" :?> string
            let typeFace = Font.OfSize(fontName, NamedSize.Default).ToTypeface()

            let tabsParent = changedView.FindViewById<TabLayout>(Resources.Id.sliding_tabs)
            let tabs = tabsParent.GetChildAt(0) :?> ViewGroup
            let tabsChildrenCount = tabs.ChildCount
            let mutable tabsChildrenCounter = 0
            while tabsChildrenCounter < tabsChildrenCount do
                let tabView = tabs.GetChildAt(tabsChildrenCounter) :?> TabLayout.TabView
                let tabChildrenCount = tabView.ChildCount
                let mutable tabChildrenCounter = 0
                while tabChildrenCounter < tabChildrenCount do
                    let tabChild = tabView.GetChildAt tabChildrenCounter
                    if tabChild <> null && tabChild.GetType() = typeof<AppCompatTextView> then
                        let textView = tabChild :?> AppCompatTextView
                        textView.SetTypeface(typeFace, Android.Graphics.TypefaceStyle.Normal)
                    tabChildrenCounter <- tabChildrenCounter + 1
                tabsChildrenCounter <- tabsChildrenCounter + 1
                

[<assembly: ExportRenderer(typeof<TabbedPage>, typeof<ExtendedTabbedPageRenderer>)>] do ()