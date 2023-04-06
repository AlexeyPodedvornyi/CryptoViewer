using CryptoViewer.Core;
using CryptoViewer.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CryptoViewer.MVVM.ViewModel
{
    public class ConverterViewModel : ObservableObject
    {
        private string _idConvertTo;
        private string _idConvertFrom;
        private double _priceFrom;
        private double _priceTo;
        private int _amount;
        private double _result;
        private List<DetailedCryptoCurrency> assets;
        private RelayCommand _calculate;
        public DetailedCryptoCurrency CryptoContext { get; }
        public double Result { get => _result; set{ _result = value; OnPropertyChanged(); } }
        public RelayCommand Calculate
        {
            get
            {
                if (_calculate == null)
                {
                    _calculate = new RelayCommand(o =>
                    {
                        Result = Convert();
                    });
                }

                if (_idConvertFrom == _idConvertTo)
                {
                    _result = 0;
                }

                return _calculate;
            }
        }

        public string IdConvertTo
        {
            get => _idConvertTo;
            set
            {
                _idConvertTo = value;
                OnPropertyChanged();
            }
        }
        public string IdConvertFrom
        {
            get => _idConvertFrom;
            set
            {
                _idConvertFrom = value;
                OnPropertyChanged();
            }
        }

        public List<DetailedCryptoCurrency> Assets
        {
            get => assets;
            set
            {
                assets = value;
                OnPropertyChanged();
            }
        }

        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        private double Convert() 
        {
            _priceFrom = Task.Run(async () => await DetailedCryptoCurrency.GetPrice(_idConvertFrom)).Result;
            _priceTo = Task.Run(async () => await DetailedCryptoCurrency.GetPrice(_idConvertTo)).Result;

            return _amount * (_priceFrom / _priceTo);
        } 


        public ConverterViewModel()
        {
            Assets = Task.Run(async () => await DetailedCryptoCurrency.GetAssets()).Result;
        }
    }
}
