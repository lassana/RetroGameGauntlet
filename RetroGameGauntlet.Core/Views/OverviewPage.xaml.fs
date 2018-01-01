namespace RetroGameGauntlet.Core.Views

open System
open System.Collections.Generic
open System.Diagnostics
open System.Threading.Tasks
open RetroGameGauntlet.Core.ViewModels
open RetroGameGauntlet.Core.ViewModels.Items
open Xamarin.Forms
open Xamarin.Forms.Xaml

type OverviewPage(targetPlatform: Option<PlatformItemViewModel>,
                  targetGame: Option<GameItemViewModel>)
                      as this =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<OverviewPage>)
    let animationDuration = 250u
    let mutable targetLayoutHeight: float = 0.0

    let viewModel = base.BindingContext :?> OverviewViewModel

    do
        if Device.RuntimePlatform = Device.iOS then
            this.DescriptionBLayout.BackgroundColor <- Color.White

        let sizeHandler = EventHandler(fun sender args -> 
            Debug.WriteLine "SizeChanged"
            AbsoluteLayout.SetLayoutBounds(this.TitleLayout, Rectangle(0.5, 1.0, this.Width, 50.0))
            targetLayoutHeight <- this.Height - 50.0 * 3.0
            let displayedView = this.GetDisplayedView()
            if displayedView.IsSome then
                displayedView.Value.HeightRequest <- targetLayoutHeight
        )
        
        //Assuming that viewmodel won't be changed
        this.SizeChanged.AddHandler sizeHandler
        viewModel.Initialized.Add(this.OnViewModelInitialized)

        async {
            if targetGame.IsSome then
                do! viewModel.InitAsync(targetGame.Value)
            else if (targetPlatform.IsSome) then
                do! viewModel.InitAsync(targetPlatform.Value)
        }
        |> Async.StartImmediate

    member this.GetViewByTitleView (titleView: View) : Option<View> =
        if titleView = (this.DescriptionATitle :> View) then
            Some (this.DescriptionALayout :> View)
        else if titleView = (this.DescriptionBTitle :> View) then
            Some (this.DescriptionBLayout :> View)
        else if titleView= (this.DescriptionCTitle :> View) then
            Some (this.DescriptionCLayout :> View)
        else
            None

    new (targetPlatform: PlatformItemViewModel) = OverviewPage(Some targetPlatform, Option.None)
    new (targetGame: GameItemViewModel) = OverviewPage(Option.None, Some targetGame)

    member this.ViewModel = base.BindingContext :?> OverviewViewModel

    member this.DescriptionATitle: Button = base.Content.FindByName<Button> "descriptionATitle"
    member this.DescriptionBTitle: Button = base.Content.FindByName<Button> "descriptionBTitle"
    member this.DescriptionCTitle: Button = base.Content.FindByName<Button> "descriptionCTitle"
    member this.DescriptionALayout: AbsoluteLayout = base.Content.FindByName<AbsoluteLayout> "descriptionALayout"
    member this.DescriptionBLayout: ListView = base.Content.FindByName<ListView> "descriptionBLayout"
    member this.DescriptionCLayout: AbsoluteLayout = base.Content.FindByName<AbsoluteLayout> "descriptionCLayout"
    member this.RootLayout: AbsoluteLayout = base.Content.FindByName<AbsoluteLayout> "rootLayout"
    member this.LoadingLayout: Frame = base.Content.FindByName<Frame> "loadingLayout"
    member this.DescriptionLayout: StackLayout = base.Content.FindByName<StackLayout> "descriptionLayout"
    member this.TitleLayout: AbsoluteLayout = base.Content.FindByName<AbsoluteLayout> "titleLayout"

    member this.Handle_WikipediaItemTapped(sender: obj, e: SelectedItemChangedEventArgs) = 
        Debug.WriteLine "Handle_WikipediaItemTapped"
        if e.SelectedItem <> null then
            let wikiPage = e.SelectedItem :?> WikipediaItemViewModel
            (sender :?> ListView).SelectedItem <- null
            viewModel.Handle_WikipediaItemTapped(wikiPage)

    member this.Handle_TitleTapped(sender: obj, e: EventArgs) = 
        Debug.WriteLine "Handle_TitleTapped"
        let senderView = sender :?> View
        let targetView = this.GetViewByTitleView(senderView)
        let displayedView = this.GetDisplayedView()
        if targetView <> displayedView then
            Debug.WriteLine "Let's animate description views"
            this.AnimateDescriptionViews(targetView, displayedView)

    member private this.OnViewModelInitialized(sender: obj, args: EventArgs) =
        Debug.WriteLine("OnViewModelInitialized")
        Device.BeginInvokeOnMainThread(fun _ ->
            this.LoadingLayout.IsVisible <- false
            this.DescriptionLayout.IsVisible <- true
            this.AnimateDescriptionViews(Some (this.DescriptionALayout :> View), None)
            async {
                //need to wait for a little time - setting the list source (wikipedia items) might break the animation
                Task.Delay(TimeSpan.FromMilliseconds((float animationDuration)*2.0)) |> Async.AwaitTask |> ignore
                do! viewModel.InitWikipediaItemsAsync()
            }
            |> Async.StartImmediate
        )

    member this.AnimateDescriptionViews(viewToShow: Option<View>, viewToHide: Option<View>) =
        if viewToShow.IsSome then
            viewToShow.Value.Opacity <- 1.0
        if viewToHide.IsSome then
            viewToHide.Value.Opacity <- 1.0
        let stepCallback = Action<float>(fun (arg: float) ->
            if viewToShow.IsSome then
                viewToShow.Value.HeightRequest <- Math.Round (arg * targetLayoutHeight)
                viewToShow.Value.Opacity <- arg
            if viewToHide.IsSome then
                viewToHide.Value.HeightRequest <- Math.Round (1.0 - arg) * targetLayoutHeight
                viewToHide.Value.Opacity <- (1.0 - arg)
            )
        let finishedCallback = Action<float, bool>(fun a b ->
            if viewToShow.IsSome then
                viewToShow.Value.HeightRequest <- targetLayoutHeight
                viewToShow.Value.Opacity <- 1.0
            if viewToHide.IsSome then
                viewToHide.Value.HeightRequest <- 0.0
                viewToHide.Value.Opacity <- 0.0
                //(viewToShow as Layout)?.ForceLayout();
                //(viewToHide as Layout)?.ForceLayout();
            )
        this.RootLayout.Animate(
            "descriptionAnimation", 
            stepCallback,
            16u,
            animationDuration,
            Easing.CubicInOut,
            finishedCallback,
            null)

    member this.GetDisplayedView() : Option<View> =
        // Sometimes after calling "view.HeightRequest = 0" value of "view.Height" may be ~0.33
        // perhaps it's a droid-specific bug
        let hiddenHeight: float = 1.0
        let hiddenOpacity: float = 0.1
        if (this.DescriptionALayout.Height > hiddenHeight 
            && this.DescriptionALayout.Opacity > hiddenOpacity) then
            Some (this.DescriptionALayout :> View)
        else if (this.DescriptionBLayout.Height > hiddenHeight 
            && this.DescriptionBLayout.Opacity > hiddenOpacity) then
            Some (this.DescriptionBLayout:> View)
        else if (this.DescriptionCLayout.Height > hiddenHeight 
            && this.DescriptionCLayout.Opacity > hiddenOpacity) then
            Some (this.DescriptionCLayout:> View)
        else None