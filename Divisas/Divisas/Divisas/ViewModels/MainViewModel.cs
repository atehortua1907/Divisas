namespace Divisas.ViewModels
{
    using Divisas.Models;
    using GalaSoft.MvvmLight.Command;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;
    using System;
    using Divisas.Services;

    public class MainViewModel : INotifyPropertyChanged
    {
        
        #region Attributes
        // Atributos dependientes para que cuando se cambien se refresquen en la viewmodel
        string _dollars;
        string _euros;
        string _pounds;
        ObservableCollection<Rate> _rates;
        bool _isEnabled;
        bool _isRunning;
        int _sourceRateId;
        int _targetRateId;
        string _amount;
        #endregion

        #region Services
        ApiService apiService;
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
                    _euros = value;
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
                    _pounds = value;
                    PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs(nameof(Pounds)));
                }
            }

        }

        public ObservableCollection<Rate> Rates { get; set; }

        public int SourceRateId { get; set; }

        public int TargetRateId { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsRunning { get; set; }

        public string Result { get; set; }

        #endregion

        #region Constructors
        public MainViewModel()
        {
            Result = "Enter an amount select source rate, select target rate and press convert button";
            apiService = new ApiService();
            LoadRates();
        }

        #endregion

        #region Commands
        public ICommand ConvertCommand
        {
            get { return new RelayCommand(Convert); }
        }

        public ICommand ConvertPlusCommand
        {
            get {return new RelayCommand(ConvertPlus); }
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

        void ConvertPlus()
        {
            throw new NotImplementedException();
        }

        async void LoadRates()
        {
            IsRunning = true;
            IsEnabled = false;
            var response = await apiService.GetRates();
            IsRunning = false;
            IsEnabled = true;

        }

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged; 
        #endregion

    }
}
