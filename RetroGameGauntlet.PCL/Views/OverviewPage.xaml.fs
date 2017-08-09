namespace RetroGameGauntlet.PCL.Views

open System
open System.Collections.Generic
open System.Threading.Tasks
open RetroGameGauntlet.PCL.ViewModels
open Xamarin.Forms
open Xamarin.Forms.Xaml

type OverviewPage(targetPlatform: Option<PlatformItemViewModel>,
                  targetGame: Option<KeyValuePair<string, string>>) =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<OverviewPage>)
    let android: bool = Device.OS = TargetPlatform.Android
    let animationDuration = 250u
    let mutable targetLayoutHeight: float = 0.0

    new (targetPlatform: PlatformItemViewModel) = OverviewPage(Some targetPlatform, Option.None)
    new (targetGame: KeyValuePair<string, string>) = OverviewPage(Option.None, Some targetGame)

    //public OverviewPage(PlatformItemViewModel targetPlatform = null,
    //                        KeyValuePair<string, string>? targetGame = null)
    //{
    //    InitializeComponent();

    //    if (Device.OS == TargetPlatform.Android)
    //    {
    //        descriptionBLayout.BackgroundColor = Color.White;
    //    }

    //    SizeChanged += OnSizeChanged;

    //    //Assume that viewmodel won't be changed
    //    ViewModel.Initialized += OnViewModelInitialized;

    //    if (targetGame != null)
    //    {
    //        ViewModel.InitAsync(targetGame.Value);
    //    }
    //    else if (targetPlatform != null)
    //    {
    //        ViewModel.InitAsync(targetPlatform);
    //    }
    //}

    //private void OnViewModelInitialized(object sender, EventArgs e)
    //{
    //    Device.BeginInvokeOnMainThread(async () =>
    //    {
    //        loadingLayout.IsVisible = false;
    //        descriptionLayout.IsVisible = true;

    //        AnimateDescriptionViews(descriptionALayout, null);
    //        //need to wait for a little time - setting the list source (wikipedia items) might break the animation
    //        await Task.Delay(AnimationDuration * 2);

    //        await ViewModel.InitListItemsAsync();
    //    });
    //}

    //private void OnSizeChanged(object sender, EventArgs e)
    //{
    //    AbsoluteLayout.SetLayoutBounds(titleLayout, new Rectangle(0.5, 1, Width, 50));

    //    _targetLayoutHeight = Height - 50 * 3;

    //    var displayedView = GetDisplayedView();
    //    if (displayedView != null)
    //        displayedView.HeightRequest = _targetLayoutHeight;
    //}

    member this.Handle_WikipediaItemTapped(sender: obj, e: SelectedItemChangedEventArgs) = 
        System.Diagnostics.Debug.WriteLine "Handle_WikipediaItemTapped"
        if e.SelectedItem <> null then
            let wikiPage = e.SelectedItem :?> WikipediaItemViewModel
            (sender :?> ListView).SelectedItem <- null
            Device.OpenUri wikiPage.Uri

    member this.Handle_TitleTapped(sender: obj, e: EventArgs) = 
        System.Diagnostics.Debug.WriteLine "Handle_TitleTapped"
    //    var targetView = GetViewByTitleView(sender as View);
    //    var displayedView = GetDisplayedView();
    //    if (targetView != displayedView)
    //    {
    //        AnimateDescriptionViews(targetView, displayedView);
    //    }

    //private void AnimateDescriptionViews(View viewToShow, View viewToHide)
    //{
    //    if (viewToShow != null)
    //    {
    //        viewToShow.Opacity = 1;
    //    }
    //    if (viewToHide != null)
    //    {
    //        viewToHide.Opacity = 1;
    //    }
    //    rootLayout.Animate("descriptionAnimation", (arg) =>
    //        {
    //            if (viewToShow != null)
    //            {
    //                viewToShow.HeightRequest = Math.Round(arg * _targetLayoutHeight);
    //                viewToShow.Opacity = arg;
    //            }
    //            if (viewToHide != null)
    //            {
    //                viewToHide.HeightRequest = Math.Round((1 - arg) * _targetLayoutHeight);
    //                viewToHide.Opacity = (1 - arg);
    //            }
    //        }, 16, AnimationDuration, Easing.CubicInOut, (a, b) =>
    //        {
    //            if (viewToShow != null)
    //            {
    //                viewToShow.HeightRequest = _targetLayoutHeight;
    //                viewToShow.Opacity = 1;
    //            }
    //            if (viewToHide != null)
    //            {
    //                viewToHide.HeightRequest = 0;
    //                viewToHide.Opacity = 0;
    //            }
    //            //(viewToShow as Layout)?.ForceLayout();
    //            //(viewToHide as Layout)?.ForceLayout();
    //        });
    //}

    //private View GetDisplayedView()
    //{
    //    // Sometimes after calling "view.HeightRequest = 0" value of "view.Height" may be ~0.33
    //    // perhaps it's a droid-specific bug
    //    const int hiddenHeight = 1;
    //    const float hiddenOpacity = 0.1f;
    //    if (descriptionALayout.Height > hiddenHeight && descriptionALayout.Opacity > hiddenOpacity)
    //    {
    //        return descriptionALayout;
    //    }
    //    else if (descriptionBLayout.Height > hiddenHeight && descriptionBLayout.Opacity > hiddenOpacity)
    //    {
    //        return descriptionBLayout;
    //    }
    //    else if (descriptionCLayout.Height > hiddenHeight && descriptionCLayout.Opacity > hiddenOpacity)
    //    {
    //        return descriptionCLayout;
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}

    //private View GetViewByTitleView(View titleView)
    //{
    //    if (titleView == descriptionATitle)
    //    {
    //        return descriptionALayout;
    //    }
    //    else if (titleView == descriptionBTitle)
    //    {
    //        return descriptionBLayout;
    //    }
    //    else if (titleView == descriptionCTitle)
    //    {
    //        return descriptionCLayout;
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}