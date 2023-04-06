using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace CryptoViewer.MVVM.Model
{
    public class Exchange
    {
        private readonly string _exchangeName;
        private readonly string _exchangeUrl;

        private string _quoteId;
        private double _quotePrice;
        private double _priceUsd;

        public string ExchangeName { get => _exchangeName; }
        public string ExchangeUrl { get => _exchangeUrl; }
        public string QuoteId { get => _quoteId; }
        public double QuotePrice { get => _quotePrice; }
        public double PriceUsd { get => _priceUsd; }

        public Exchange(string exchangeId, string? explorer, string quoteId, double? priceQuote, double? priceUsd)
        {
            _exchangeName = exchangeId;
            _quoteId = quoteId;
            if(priceUsd != null)
            {
                _priceUsd = Math.Round((double)priceUsd, 10);
            }
            else
            {
                _priceUsd = 0.00;
            }

            if (explorer != null)
            {
                _exchangeUrl = explorer;
            }
                   
            if(priceQuote != null)
            {
                _quotePrice = Math.Round((double)priceQuote, 10);
            }
            else
            {
                _quotePrice = 0.00;
            }
                    
        }
    }
}
