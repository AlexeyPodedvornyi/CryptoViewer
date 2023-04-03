using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoViewer.MVVM.Model
{
    public class DetailedCryptoCurrency : CryptoCurrency
    {
        private readonly double _volume;
        private readonly double _priceChange;

        public DetailedCryptoCurrency(int rank, string name, double priceUsd) : base(rank, name, priceUsd)
        {
        }
    }
}
