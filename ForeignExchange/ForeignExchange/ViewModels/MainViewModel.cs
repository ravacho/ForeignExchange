using ForeignExchange.Helpers;
using ForeignExchange.Models;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ForeignExchange.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region MyRegion Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes
        bool _isRunning;
        string _result;
        string _status;
        bool _isEnabled;
        Rate _sourceRate;
        Rate _targetRate;
        ObservableCollection<Rate> _rates;
        #endregion

        #region Properties
        public string Status
        { 
            get
            {
                return _status;
            }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Status)));
                }
            }
        }
        public string Amount
        {
            get;
            set;
        }

        public Rate SourceRate
        {
            get
            {
                return _sourceRate;
            }
            set
            {
                if (_sourceRate != value)
                {
                    _sourceRate = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(SourceRate)));
                }
            }
        }

        public Rate TargetRate
        {
            get
            {
                return _targetRate;
            }
            set
            {
                if (_targetRate != value)
                {
                    _targetRate = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(TargetRate)));
                }
            }
        }

        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }
        public ObservableCollection<Rate> Rates
        {
            get
            {
                return _rates;
            }
            set
            {
                if (_rates != value)
                {
                    _rates = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Rates)));
                }
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }

        }

        public string Result
        {
            get
            {
                return _result;
            }
            set
            {
                if (_result != value)
                {
                    _result = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result)));
                }
            }
        }

        #endregion

        #region Constructors
        public MainViewModel()
        {
            LoadRatesAsync();
        }
        #endregion

        #region Methods
        //async System.Threading.Tasks.Task LoadRatesAsync()
        private void LoadRatesAsync()
        {
            IsRunning = true;
            Result = "Loading rates...";
            
            try
            {
                //var client = new HttpClient();
                //client.BaseAddress = new Uri("http://exchage.net");
                //var controller = "api/rates";
                //var response = await client.GetAsync(controller);
                //var result = await response.Content.ReadAsStringAsync();
                //if (!response.IsSuccessStatusCode)
                //{
                //    IsRunning = false;
                //    Result = result;
                //}
                var result = GetResult();
                var rates = JsonConvert.DeserializeObject<List<Rate>>(result);
                Rates = new ObservableCollection<Rate>(rates);
               
                IsRunning = false;
                IsEnabled = true;
                Result = "Ready to Convert!!";

            }
            catch (Exception ex)
            {
                IsRunning = true;
                Result = ex.Message;
            }
        }

        private string GetResult()
        {
            List<Rate> rates = new List<Rate>();
            rates.Add(new Rate(1, "EUR", 1.2, "Euros"));
            rates.Add(new Rate(2, "DOL", 1, "Dolares"));
            rates.Add(new Rate(3, "BOL", 150, "Bolivares"));
            rates.Add(new Rate(3, "DAJ", 0.792, "DAJERS"));

            return JsonConvert.SerializeObject(rates);
        }
        #endregion

        #region Commands
        public ICommand SwithCommand
        {
            get
            {
                return new RelayCommand(Switch);
            }
        }

        void Switch()
        {
            var aux = SourceRate;
            SourceRate = TargetRate;
            TargetRate = aux;
            Convert();
        }

        public ICommand ConvertCommand
        {
            get
            {
                return new RelayCommand(Convert);
            }
        }

        async void Convert()
        {
            if (string.IsNullOrEmpty(Amount))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    Lenguages.AmountValidation,
                    "Accept");
                return;
            }

            decimal amount = 0;
            if (!decimal.TryParse(Amount, out amount))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a numeric value in amount.",
                    "Accept");
                return;
            }

            if (SourceRate == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must select a source rate.",
                    "Accept");
                return;
            }

            if (TargetRate == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must select a target rate.",
                    "Accept");
                return;
            }

            var amountConverted = amount / (decimal)SourceRate.TaxRate * (decimal)TargetRate.TaxRate;

            Result = string.Format(
                    "{0} {1:C2} = {2} {3:C2}", 
                    SourceRate.Code, 
                    amount, 
                    TargetRate.Code, 
                    amountConverted);
        }
        #endregion

    }
}
