using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RetroGameGauntlet.Forms.ViewModels;
using Xamarin.Forms;

namespace RetroGameGauntlet.Forms.Views
{
    public partial class OverviewPage : ContentPage
    {
        public OverviewViewModel ViewModel { get { return BindingContext as OverviewViewModel; } }

        private const int AnimationDuration = 250;

        private double _targetLayoutHeight;

        public OverviewPage(PlatformItemViewModel targetPlatform = null,
                            KeyValuePair<string, string>? targetGame = null)
        {
            InitializeComponent();

            if (Device.OS == TargetPlatform.Android)
            {
                descriptionBLayout.BackgroundColor = Color.White;
            }

            SizeChanged += OnSizeChanged;

            //Assume that viewmodel won't be changed
            ViewModel.Initialized += OnViewModelInitialized;

            if (targetGame != null)
            {
                ViewModel.InitAsync(targetGame.Value);
            }
            else if (targetPlatform != null)
            {
                ViewModel.InitAsync(targetPlatform);
            }
        }

        private void OnViewModelInitialized(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                loadingLayout.IsVisible = false;
                descriptionLayout.IsVisible = true;

                AnimateDescriptionViews(descriptionALayout, null);
                //need to wait for a little time - setting the list source (wikipedia items) might break the animation
                await Task.Delay(AnimationDuration * 2);

                await ViewModel.InitListItemsAsync();
            });
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            AbsoluteLayout.SetLayoutBounds(titleLayout, new Rectangle(0.5, 1, Width, 50));

            _targetLayoutHeight = Height - 50 * 3;

            var displayedView = GetDisplayedView();
            if (displayedView != null)
                displayedView.HeightRequest = _targetLayoutHeight;
        }

        private void OnWikipediaItemTap(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            var wikiPage = e.SelectedItem as WikipediaItemViewModel;
            Device.OpenUri(wikiPage.Uri);
            ((ListView)sender).SelectedItem = null;
        }

        private void OnTitleClicked(object sender, EventArgs e)
        {
            var targetView = GetViewByTitleView(sender as View);
            var displayedView = GetDisplayedView();
            if (targetView != displayedView)
            {
                AnimateDescriptionViews(targetView, displayedView);
            }
        }

        private void AnimateDescriptionViews(View viewToShow, View viewToHide)
        {
            if (viewToShow != null)
            {
                viewToShow.Opacity = 1;
            }
            if (viewToHide != null)
            {
                viewToHide.Opacity = 1;
            }
            rootLayout.Animate("descriptionAnimation", (arg) =>
                {
                    if (viewToShow != null)
                    {
                        viewToShow.HeightRequest = Math.Round(arg * _targetLayoutHeight);
                        viewToShow.Opacity = arg;
                    }
                    if (viewToHide != null)
                    {
                        viewToHide.HeightRequest = Math.Round((1 - arg) * _targetLayoutHeight);
                        viewToHide.Opacity = (1 - arg);
                    }
                }, 16, AnimationDuration, Easing.CubicInOut, (a, b) =>
                {
                    if (viewToShow != null)
                    {
                        viewToShow.HeightRequest = _targetLayoutHeight;
                        viewToShow.Opacity = 1;
                    }
                    if (viewToHide != null)
                    {
                        viewToHide.HeightRequest = 0;
                        viewToHide.Opacity = 0;
                    }
                    //(viewToShow as Layout)?.ForceLayout();
                    //(viewToHide as Layout)?.ForceLayout();
                });
        }

        private View GetDisplayedView()
        {
            // Sometimes after calling "view.HeightRequest = 0" value of "view.Height" may be ~0.33
            // perhaps it's a droid-specific bug
            const int hiddenHeight = 1;
            const float hiddenOpacity = 0.1f;
            if (descriptionALayout.Height > hiddenHeight && descriptionALayout.Opacity > hiddenOpacity)
            {
                return descriptionALayout;
            }
            else if (descriptionBLayout.Height > hiddenHeight && descriptionBLayout.Opacity > hiddenOpacity)
            {
                return descriptionBLayout;
            }
            else if (descriptionCLayout.Height > hiddenHeight && descriptionCLayout.Opacity > hiddenOpacity)
            {
                return descriptionCLayout;
            }
            else
            {
                return null;
            }
        }

        private View GetViewByTitleView(View titleView)
        {
            if (titleView == descriptionATitle)
            {
                return descriptionALayout;
            }
            else if (titleView == descriptionBTitle)
            {
                return descriptionBLayout;
            }
            else if (titleView == descriptionCTitle)
            {
                return descriptionCLayout;
            }
            else
            {
                return null;
            }
        }
    }
}
