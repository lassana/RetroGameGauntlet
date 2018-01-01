namespace RetroGameGauntlet.Droid.Effects

open Android.Util
open Xamarin.Forms
open Xamarin.Forms.Platform.Android

type TransparentSelectableDroidEffect () =
    inherit PlatformEffect ()

    override this.OnAttached() =
        let value = new TypedValue()
        let resolved = Android.App.Application.Context.Theme.ResolveAttribute(Android.Resource.Attribute.SelectableItemBackground, value, true)
        if resolved then
            base.Control.SetBackgroundResource(value.ResourceId)

    override this.OnDetached() = ()

[<assembly: ResolutionGroupName("RetroGameGauntlet")>] do ()
[<assembly: ExportEffect(typeof<TransparentSelectableDroidEffect>, "TransparentSelectableEffect")>] do ()