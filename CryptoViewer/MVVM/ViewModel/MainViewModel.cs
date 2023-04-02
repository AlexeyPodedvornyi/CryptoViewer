using CryptoViewer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CryptoViewer.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
		private object _currentView;

        #region Navigation commands
        public RelayCommand NavigateToHome { get; set; }
        public RelayCommand NavigateToInfo { get; set; }
        public RelayCommand NavigateToConverter { get; set; }
        public RelayCommand NavigateToSearch { get; set; }
        #endregion

        #region View models
        public HomeViewModel HomeVM { get; set; }
        public InfoViewModel InfoVM { get; set; }

        public ConverterViewModel ConverterVM { get; set; }
        public SearchViewModel SearchVM { get; set; }

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
			HomeVM = new HomeViewModel();
            InfoVM = new InfoViewModel();
            ConverterVM = new ConverterViewModel();
            SearchVM = new SearchViewModel();

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

            NavigateToSearch = new RelayCommand(o =>
            {
                CurrentView = SearchVM;
            });
        }
    }
}
