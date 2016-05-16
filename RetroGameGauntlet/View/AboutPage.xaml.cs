using System;

using Xamarin.Forms;
using RetroGameGauntlet.Core;

namespace RetroGameGauntlet.View
{
    public partial class AboutPage : ContentPage
    {
        private Point? forkLabelPoint;
        private double forkLabelSide;
        private bool isDisplayed;

        public AboutPage()
        {
            InitializeComponent();

            if (Device.OS == TargetPlatform.iOS)
            {
                Icon = "ico_notepad.png";
            }

            SetForkLabelOpacity(0);

            Device.OnPlatform(
                iOS: () =>
                {
                    SizeChanged += MoveForkLabel;
                }, 
                Android: () =>
                {
                    SizeChanged += MoveForkLabel;
                    forkLabel.SizeChanged += MoveForkLabel;
                });
        }

        private void MoveForkLabel(object sender, EventArgs e)
        {
            bool finalState = Width > 0 && Height > 0 && forkLabel.Width > 0 && forkLabel.Height > 0;
            if (!finalState)
            {
                return;
            }

            forkLabelSide = (forkLabel.Width / Math.Sqrt(2));
            var x = Width - (forkLabel.Width / Math.Sqrt(2)) + (forkLabel.Height / (Math.Sqrt(2)));
            var y = -forkLabel.Height / Math.Sqrt(2);

            forkLabel.AnchorX = 0;
            forkLabel.AnchorY = 0;
            forkLabel.Rotation = 45;

            forkLabelPoint = new Point(x, y);

            if (forkLabel.Opacity > 0 && isDisplayed)
            {
                AnimateForkLabel();
            }
        }

        private void OnForkTapped(object sender, EventArgs args)
        {
            Device.OpenUri(new Uri("https://github.com/lassana/RetroGameGauntlet"));
        }

        private async void AnimateForkLabel()
        {
            if (forkLabelPoint != null)
            {
                SetForkLabelOpacity(0);
                await forkLabel.TranslateTo(forkLabelPoint.Value.X + forkLabelSide, forkLabelPoint.Value.Y - forkLabelSide, 0, Easing.Linear);
                SetForkLabelOpacity(1);
                await forkLabel.TranslateTo(forkLabelPoint.Value.X, forkLabelPoint.Value.Y, 250 * 2, Easing.CubicInOut);
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            isDisplayed = true;
            AnimateForkLabel();
            var newRandomImage = await new FlickImageSearch().GetImage();
            if (newRandomImage != (randomImage.Source as UriImageSource)?.Uri?.AbsoluteUri)
            {
                randomImage.Source = newRandomImage;
            }
        }

        private void SetForkLabelOpacity(double value)
        {
            if(Device.OS == Xamarin.Forms.TargetPlatform.iOS)
            {
                forkLabel.Opacity = value;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            isDisplayed = false;
            SetForkLabelOpacity(0);
            if (forkLabelPoint != null)
            {
                forkLabel.TranslateTo(forkLabelPoint.Value.X + forkLabelSide, forkLabelPoint.Value.Y - forkLabelSide, 0, Easing.Linear);
            }
        }
    }
}

