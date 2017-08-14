

namespace Divisas.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class MainViewModel : INotifyPropertyChanged
    {
        #region Attributes
        string _dollars;
        string _euros;
        string _pounds;
        #endregion


        #region Properties
        public string Pesos { get; set; }

        public string Dollars
        {
            get { return _dollars; }
            set
            {
                if (value != _dollars)
                {
                    _dollars = value;
                    PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs(nameof(Dollars)));
                }
            }
        }

        public string Euros
        {
            get { return _euros; }
            set
            {
                if (value != _euros)
                {
                    _dollars = value;
                    PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs(nameof(Euros)));
                }
            }
        }

        public string Pounds
        {
            get { return _pounds; }
            set
            {
                if (value != _pounds)
                {
                    _dollars = value;
                    PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs(nameof(Pounds)));
                }
            }
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
        }
        #endregion

        #region Commands
        public ICommand ConvertCommand
        {
            get { return new RelayCommand(Convert); }
        }
        #endregion

        #region Methods
        async void Convert()
        {
            if (string.IsNullOrEmpty(Pesos))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a value numeric in pesos",
                    "Accept");
                return;
            }

            decimal pesos = 0;
            if (!decimal.TryParse(Pesos, out pesos))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a value numeric in pesos",
                    "Accept");
                return;
            }

            var dollars = pesos / 2994.01198M;
            var euros = pesos / 3514.95509M;
            var pounds = pesos / 3894.50599M;

            Dollars = string.Format("{0:C2}", dollars);
            Euros = string.Format("{0:C2}", euros);
            Pounds = string.Format("{0:C2}", pounds);
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged; 
        #endregion

    }
}
