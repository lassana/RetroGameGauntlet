using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace RetroGameGauntlet.Forms.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                RaisePropertyChanged(nameof(IsLoading));
            }
        }

        private string _errorText;
        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                _errorText = value;
                RaisePropertyChanged(nameof(ErrorText));
            }
        }

        public event EventHandler PopAsyncRequested;

        public event EventHandler<Page> PushAsyncRequested;

        protected void PopPage()
        {
            PopAsyncRequested?.Invoke(this, EventArgs.Empty);
        }

        protected void PushPage(Page page)
        {
            PushAsyncRequested?.Invoke(this, page);
        }

        protected BaseViewModel()
        {
        }

        protected BaseViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        protected void RaisePropertyChanged(params string[] propertyNames)
        {
            if (PropertyChanged != null && propertyNames != null)
            {
                foreach (string propertyName in propertyNames)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }
    }
}