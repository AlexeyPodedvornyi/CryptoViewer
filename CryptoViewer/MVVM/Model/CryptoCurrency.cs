using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CryptoViewer.MVVM.Model
{
    public class CryptoCurrency
    {
        private readonly string _name;
        private readonly double _price;
        private readonly int _rank;

        public string Name { get => _name; }
        public double Price { get => _price; }
        public int Rank { get => _rank; }

        public CryptoCurrency(int rank, string name, double priceUsd)
        {
            _name = name;
            _price = priceUsd;
            _rank = rank;
        }

        public static async Task<List<CryptoCurrency>> GetTop10Currencies()
        {
            try
            {
                var response = await HttpRequester.CurrentRequestor.GetRequest("https://api.coincap.io/v2/assets?limit=10");              
                return JsonConvert.DeserializeObject<List<CryptoCurrency>>(response["data"].ToString());  
            }
            catch (HttpRequestException)
            {
                return new List<CryptoCurrency>();
            }
            catch (ArgumentNullException)
            {
                return new List<CryptoCurrency>();
            }
        }

        override public string ToString()
        {
            return $"{_rank} {_name} {_price}";
            
        }

    }
}

