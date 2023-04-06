using CryptoViewer.Core;
using CryptoViewer.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoViewer.MVVM.View;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Input;
using System.Windows.Documents;
using System.Windows;

namespace CryptoViewer.MVVM.ViewModel
{
    public class HomeViewModel : ObservableObject
    {
        private List<DetailedCryptoCurrency> _topCurrencies;
        private RelayCommand _sendStringToParent;
        private RelayCommand _sendCurrencyToParent;
        private string _searchRequest;
        private DetailedCryptoCurrency _selectedCurrency;

        public string SearchRequest
        {
            get => _searchRequest; 
            set 
            { 
                _searchRequest = value;
                OnPropertyChanged(); 
            }
        }
        public DetailedCryptoCurrency SelectedCurrency
        {
            get => _selectedCurrency;
            set
            {
                _selectedCurrency = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand SendStringToParent
        {
            get
            {
                if(_sendStringToParent == null)
                {
                    _sendStringToParent = new RelayCommand(o =>
                    {
                        _parentViewModel.Data=SearchRequest;

                    });
                }
                return _sendStringToParent;
            }
            set => _sendStringToParent = value;
        }
        public RelayCommand SendCurrencyToParent
        {
            get
            {
                if (_sendCurrencyToParent == null)
                {
                    _sendCurrencyToParent = new RelayCommand(o =>
                    {
                        _parentViewModel.SelectedCurr = SelectedCurrency;
                    });
                }
                return _sendCurrencyToParent;
            }
            set => _sendCurrencyToParent = value;
        }

        public List<DetailedCryptoCurrency> TopCurrencies
        {
            get => _topCurrencies;
            set
            {
                _topCurrencies = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel()
        {
            
        }

        private readonly MainViewModel _parentViewModel;
        public HomeViewModel(MainViewModel parentViewModel)
        {
            _parentViewModel = parentViewModel;
            _topCurrencies = Task.Run(async () => await DetailedCryptoCurrency.GetTop10Currencies()).Result;
        }

    }
}
