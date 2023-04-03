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

namespace CryptoViewer.MVVM.ViewModel
{
    public class HomeViewModel : ObservableObject
    {
        private List<CryptoCurrency> _topCurrencies;

        public List<CryptoCurrency> TopCurrencies
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
            _topCurrencies = Task.Run(async () => await CryptoCurrency.GetTop10Currencies()).Result;

            //TODO: add some condition if List still empty
        }
    }
}
