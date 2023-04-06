using CryptoViewer.Core;
using CryptoViewer.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CryptoViewer.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
		private object _currentView;

        #region Navigation commands
        public RelayCommand NavigateToHome { get; set; }
        public RelayCommand NavigateToInfo { get; set; }
        public RelayCommand NavigateToConverter { get; set; }
        #endregion

        #region View models
        public HomeViewModel HomeVM { get; set; }
        public InfoViewModel InfoVM { get; set; }

        public ConverterViewModel ConverterVM { get; set; }

        #endregion

        public object CurrentView
		{
			get  => _currentView; 
			set { 
				_currentView = value; 
				OnPropertyChanged();
			}
		}

        public MainViewModel()
        {

            HomeVM = new HomeViewModel(this);
            InfoVM = new InfoViewModel(this);
            ConverterVM = new ConverterViewModel();

            _currentView = HomeVM;


            NavigateToHome = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            NavigateToInfo = new RelayCommand(o =>
            {
                CurrentView = InfoVM;
            });

            NavigateToConverter = new RelayCommand(o =>
            {
                CurrentView = ConverterVM;
            });
        }

        private string _data;
        private DetailedCryptoCurrency _selected;
        public string Data
        {
            get { return _data; }
            set { _data = value; OnPropertyChanged(nameof(Data)); }
        }

        public DetailedCryptoCurrency SelectedCurr
        {
            get => _selected;
            set { _selected = value; OnPropertyChanged(nameof(SelectedCurr));}
        }


    }
}
