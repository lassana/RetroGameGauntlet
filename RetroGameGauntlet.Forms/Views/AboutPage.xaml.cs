using System;
using Xamarin.Forms;
using RetroGameGauntlet.Forms.ViewModels;

namespace RetroGameGauntlet.Forms.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutViewModel ViewModel { get { return BindingContext as AboutViewModel; } }

        private Point? _forkLabelPoint;
        private double _forkLabelSide;
        private bool _isDisplayed;

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

            _forkLabelSide = (forkLabel.Width / Math.Sqrt(2));
            var x = Width - (forkLabel.Width / Math.Sqrt(2)) + (forkLabel.Height / (Math.Sqrt(2)));
            var y = -forkLabel.Height / Math.Sqrt(2);

            forkLabel.AnchorX = 0;
            forkLabel.AnchorY = 0;
            forkLabel.Rotation = 45;

            _forkLabelPoint = new Point(x, y);

            if (forkLabel.Opacity > 0 && _isDisplayed)
            {
                AnimateForkLabel();
            }
        }

        private async void AnimateForkLabel()
        {
            if (_forkLabelPoint != null)
            {
                SetForkLabelOpacity(0);
                await forkLabel.TranslateTo(_forkLabelPoint.Value.X + _forkLabelSide, _forkLabelPoint.Value.Y - _forkLabelSide, 0, Easing.Linear);
                SetForkLabelOpacity(1);
                await forkLabel.TranslateTo(_forkLabelPoint.Value.X, _forkLabelPoint.Value.Y, 250 * 2, Easing.CubicInOut);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _isDisplayed = true;
            AnimateForkLabel();
            Device.BeginInvokeOnMainThread(async () => await ViewModel.InitAsync());
        }

        private void SetForkLabelOpacity(double value)
        {
            if (Device.OS == TargetPlatform.iOS)
            {
                forkLabel.Opacity = value;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _isDisplayed = false;
            SetForkLabelOpacity(0);
            if (_forkLabelPoint != null)
            {
                forkLabel.TranslateTo(_forkLabelPoint.Value.X + _forkLabelSide, _forkLabelPoint.Value.Y - _forkLabelSide, 0, Easing.Linear);
            }
        }
    }
}
