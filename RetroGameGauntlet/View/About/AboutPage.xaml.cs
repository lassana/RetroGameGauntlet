using System;

using Xamarin.Forms;

namespace View.About
{
    public partial class AboutPage : ContentPage, ITabbedPageChild
    {
        private Point? forkLabelPoint;
        private double forkLabelSide;


        public AboutPage()
        {
            InitializeComponent();

            forkLabel.SizeChanged += MoveForkLabel;
        }

        private void MoveForkLabel(object sender, EventArgs e)
        {
            forkLabel.SizeChanged -= MoveForkLabel;

            forkLabelSide = (forkLabel.Width / Math.Sqrt(2));
            var x = Width - (forkLabel.Width / Math.Sqrt(2)) + (forkLabel.Height / (Math.Sqrt(2)));
            var y = -forkLabel.Height / Math.Sqrt(2);

            forkLabel.AnchorX = 0;
            forkLabel.AnchorY = 0;

            forkLabel.Rotation = 45;


            forkLabelPoint = new Point(x, y);

            forkLabel.TranslateTo(forkLabelPoint.Value.X+forkLabelSide, forkLabelPoint.Value.Y-forkLabelSide, 0, Easing.Linear);
        }

        private void OnForkTapped(object sender, EventArgs args) {
            Device.OpenUri(new Uri("https://github.com/lassana/RetroGameGauntlet"));
        }

        private void AnimateForkLabel()
        {
            if (forkLabelPoint != null)
            {
                forkLabel.TranslateTo(forkLabelPoint.Value.X, forkLabelPoint.Value.Y, 250 * 2, Easing.CubicInOut);
            }
        }

        #region ITabbedPageChild implementation

        public void Opened()
        {
            AnimateForkLabel();
        }

        public void Closed()
        {
            forkLabel.TranslateTo(forkLabelPoint.Value.X + forkLabelSide, forkLabelPoint.Value.Y - forkLabelSide, 0, Easing.Linear);
        }

        #endregion
    }
}

