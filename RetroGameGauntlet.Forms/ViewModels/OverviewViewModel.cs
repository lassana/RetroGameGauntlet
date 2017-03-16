using System;
namespace RetroGameGauntlet.Forms.ViewModels
{
    public class OverviewViewModel : BaseViewModel
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        public OverviewViewModel()
        {
            Title = "Loading...";
        }
    }
}
