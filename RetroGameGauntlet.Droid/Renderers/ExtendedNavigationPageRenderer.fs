namespace RetroGameGauntlet.Droid.Renderers

open System
open System.ComponentModel
open Android.Content
open Android.Support.V7.Widget
open Android.Views
open Xamarin.Forms
open Xamarin.Forms.Platform.Android
open Xamarin.Forms.Platform.Android.AppCompat

/// The NavigationPage renderer that replaces the text style of a toolbar tilte.
type ExtendedNavigationPageRenderer (context: Context) as this =
    inherit NavigationPageRenderer (context)

    let mutable toolbarView: Toolbar = null
    let mutable titleView: AppCompatTextView = null

    let toolbarChildrenObserver = EventHandler<ViewGroup.ChildViewAddedEventArgs>(fun sender args -> 
        this.OnToolbarChildViewAdded(sender, args)
        )

    override this.OnViewAdded(child: Android.Views.View) =
        base.OnViewAdded(child)
        if child.GetType() = typeof<Toolbar> then
            toolbarView <- child :?> Toolbar
            toolbarView.ChildViewAdded.AddHandler toolbarChildrenObserver

    override this.OnElementPropertyChanged(sender: obj, e: PropertyChangedEventArgs) =
        base.OnElementPropertyChanged(sender, e)

        let needUpdateToolbar = 
            if (e.PropertyName = "Renderer" || e.PropertyName = "CurrentPage") then
                true
            else if (e.PropertyName = "Width" || e.PropertyName = "Height") then
                this.Width > 0 && this.Height > 0
            else
                false

        if needUpdateToolbar then this.UpdateToolbar()

    member this.OnToolbarChildViewAdded(sender: obj, args: ViewGroup.ChildViewAddedEventArgs) =
        if args.Child.GetType() = typeof<AppCompatTextView> then
            titleView <- (args.Child :?> AppCompatTextView)
            toolbarView.ChildViewAdded.RemoveHandler toolbarChildrenObserver
            this.UpdateToolbar()

    member this.UpdateToolbar () =
        //HACK need to test it on each Xamarin.Forms update
        if toolbarView <> null && titleView <> null then
            System.Diagnostics.Debug.WriteLine "Updating toolbar font..."
            let fontName = Application.Current.Resources.Item "monoFont" :?> string
            let typeFace = Font.OfSize(fontName, NamedSize.Default).ToTypeface()
            titleView.SetTypeface(typeFace, Android.Graphics.TypefaceStyle.Normal)
            titleView.SetTextSize(Android.Util.ComplexUnitType.Sp, float32 14.0)


[<assembly: ExportRenderer(typeof<NavigationPage>, typeof<ExtendedNavigationPageRenderer>)>] do ()