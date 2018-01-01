namespace RetroGameGauntlet.PCL.Views

open RetroGameGauntlet.PCL.ViewModels
open System
open System.Diagnostics
open Xamarin.Forms
open Xamarin.Forms.Xaml

type AboutPage() as this = 
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<AboutPage>)
    let mutable isVisible: bool = false
    let mutable forkLabelPoint: Option<Point> = None
    let mutable forkLabelSide: float = 0.0

    do
        if Device.RuntimePlatform = Device.iOS then
            base.Icon <- FileImageSource(File="ico_notepad.png")
            let hdlr = EventHandler(fun sender args -> 
                let finalState = this.Width > 0.0
                                 && this.Height > 0.0
                                 && this.ForkLabel.Width > 0.0
                                 && this.ForkLabel.Height > 0.0
                if finalState then
                    forkLabelSide <- this.ForkLabel.Width / Math.Sqrt(2.0)
                    let x = this.Width - (this.ForkLabel.Width / Math.Sqrt(2.0)) + (this.ForkLabel.Height / Math.Sqrt(2.0))
                    let y = -this.ForkLabel.Height / Math.Sqrt(2.0)

                    this.ForkLabel.AnchorX <- 0.0
                    this.ForkLabel.AnchorY <- 0.0
                    this.ForkLabel.Rotation <- 45.0

                    forkLabelPoint <- Some(Point(x,y))

                    if this.ForkLabelOpacity > 0.0 && isVisible then 
                        this.AnimateForkLabel()
                )
            this.SizeChanged.AddHandler hdlr

    member this.ViewModel = base.BindingContext :?> AboutViewModel

    member this.ForkLabel: Label = base.Content.FindByName<Label> "forkLabel"

    member this.ForkLabelOpacity 
        with get() = this.ForkLabel.Opacity
        and set parameter = 
            if Device.RuntimePlatform = Device.iOS then
                this.ForkLabel.Opacity <- parameter

    member this.AnimateForkLabel() =
        if Device.RuntimePlatform = Device.iOS then
            if forkLabelPoint.IsSome then
                async {
                    this.ForkLabelOpacity <- 0.0
                    do!
                        this.ForkLabel.TranslateTo(forkLabelPoint.Value.X + forkLabelSide, forkLabelPoint.Value.Y - forkLabelSide, 0u, Easing.Linear)
                        |> Async.AwaitTask
                        |> Async.Ignore
                    this.ForkLabelOpacity <- 1.0
                    this.ForkLabel.IsVisible <- true
                    do!
                        this.ForkLabel.TranslateTo(forkLabelPoint.Value.X, forkLabelPoint.Value.Y, 450u, Easing.CubicInOut) 
                        |> Async.AwaitTask
                        |> Async.Ignore
                } |> Async.StartImmediate
        else
            this.ForkLabel.IsVisible <- false

    override this.OnAppearing() = 
        base.OnAppearing()
        isVisible <- true
        Debug.WriteLine "OnAppearing"
        this.AnimateForkLabel()
        async {
            do! this.ViewModel.InitAsync()
        } |> Async.StartImmediate

    override this.OnDisappearing() =
        base.OnDisappearing()
        isVisible <- false
        this.ForkLabelOpacity <- 0.0
        async {
            if forkLabelPoint.IsSome then
                this.ForkLabel.TranslateTo(forkLabelPoint.Value.X + forkLabelSide, forkLabelPoint.Value.Y - forkLabelSide, 0u, Easing.Linear) 
                |> Async.AwaitTask 
                |> ignore
        }
        |> Async.StartImmediate