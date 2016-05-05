using System;

using Xamarin.Forms;
using Core;
using System.Threading.Tasks;

namespace View.About
{
    public partial class AboutPage : ContentPage
    {
        private Point? forkLabelPoint;
        private double forkLabelSide;


        public AboutPage()
        {
            InitializeComponent();

            forkLabel.Opacity = 0;

            SizeChanged += MoveForkLabel;
        }

        private void MoveForkLabel(object sender, EventArgs e)
        {
            forkLabelSide = (forkLabel.Width / Math.Sqrt(2));
            var x = Width - (forkLabel.Width / Math.Sqrt(2)) + (forkLabel.Height / (Math.Sqrt(2)));
            var y = -forkLabel.Height / Math.Sqrt(2);

            forkLabel.AnchorX = 0;
            forkLabel.AnchorY = 0;
            forkLabel.Rotation = 45;

            forkLabelPoint = new Point(x, y);
            if (forkLabel.Opacity > 0)
            {
                AnimateForkLabel();
            }
        }

        private void OnForkTapped(object sender, EventArgs args) {
            Device.OpenUri(new Uri("https://github.com/lassana/RetroGameGauntlet"));
        }

        private async void AnimateForkLabel()
        {
            if (forkLabelPoint != null)
            {
                forkLabel.Opacity = 0;
                await forkLabel.TranslateTo(forkLabelPoint.Value.X + forkLabelSide, forkLabelPoint.Value.Y - forkLabelSide, 0, Easing.Linear);
                forkLabel.Opacity = 1;
                await forkLabel.TranslateTo(forkLabelPoint.Value.X, forkLabelPoint.Value.Y, 250 * 2, Easing.CubicInOut);
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            AnimateForkLabel();
            randomImage.Source = await new FlickImageSearch().GetImage();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            forkLabel.Opacity = 0;
            if (forkLabelPoint != null)
            {
                forkLabel.TranslateTo(forkLabelPoint.Value.X + forkLabelSide, forkLabelPoint.Value.Y - forkLabelSide, 0, Easing.Linear);
            }
        }
    }
}

