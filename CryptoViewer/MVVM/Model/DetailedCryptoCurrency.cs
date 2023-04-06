using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CryptoViewer.MVVM.Model
{
    public class DetailedCryptoCurrency
    {
        private string _id;
        private double? _volume;
        private double? _priceChange;
        private List<Exchange> _exchanges;
        private string _name;
        private double _price;
        private int _rank;

        public string Name { get => _name; set => _name = value;  }
        public double Price { get => _price; set => _price = value; }
        public int Rank { get => _rank; set => _rank = value; }

        public string Id { get => _id; set => _id = value; }
        public double? Volume { get => _volume; set => _volume = value; }
        public double? PriceChange { get => _priceChange; set => _priceChange = value; }
        public List<Exchange> Exchanges { get => _exchanges; set => _exchanges = value; }

        public DetailedCryptoCurrency(string id, int rank, string name, double priceUsd, double? volumeUsd24Hr, double? changePercent24Hr)
        {
            _name = name;
            _price = priceUsd;
            _rank = rank;
            _id = id;
            if (volumeUsd24Hr != null)
            {
                _volume = volumeUsd24Hr;
            }
            if (changePercent24Hr != null)
            {
                _priceChange = changePercent24Hr;
            }        
            _exchanges = Task.Run(async () => await GetAvailableExchanges()).Result;
        }

        private async Task<List<Exchange>> GetAvailableExchanges()
        {
            HttpRequester requester = new HttpRequester();
            try
            {
                var response = await requester.GetRequest($"https://api.coincap.io/v2/markets?exchangeOnly=true&baseId={_id}");
                return JsonConvert.DeserializeObject<List<Exchange>>(response["data"].ToString());
            }
            catch (HttpRequestException)
            {
                return new List<Exchange>();
            }
            catch (ArgumentNullException)
            {
                return new List<Exchange>();
            }
        }

        public static async Task<List<DetailedCryptoCurrency>> GetTop10Currencies()
        {
            HttpRequester requester = new HttpRequester();
            try
            {
                var response = await requester.GetRequest("https://api.coincap.io/v2/assets?limit=10");
                return JsonConvert.DeserializeObject<List<DetailedCryptoCurrency>>(response["data"].ToString());
            }
            catch (HttpRequestException)
            {
                return new List<DetailedCryptoCurrency>();
            }
            catch (ArgumentNullException)
            {
                return new List<DetailedCryptoCurrency>();
            }
        }

        public static async Task<List<DetailedCryptoCurrency>> GetByNameOrId(string value)
        {   
            HttpRequester requester = new HttpRequester();
            try
            {
                var response = await requester.GetRequest($"https://api.coincap.io/v2/assets?search={value}");
                return JsonConvert.DeserializeObject<List<DetailedCryptoCurrency>>(response["data"].ToString());
            }
            catch (HttpRequestException)
            {
                return new List<DetailedCryptoCurrency>();
            }
            catch (ArgumentNullException)
            {
                return new List<DetailedCryptoCurrency>();
            }
        }

        public static async Task<double> GetPrice(string id)
        {
            HttpRequester requester = new HttpRequester();
            try
            {
                var response = await requester.GetRequest($"https://api.coincap.io/v2/assets/{id}");
                var a = JsonConvert.DeserializeObject<DetailedCryptoCurrency>(response["data"].ToString());
                return a.Price;
            }
            catch (HttpRequestException)
            {
                return 0.00;
            }
            catch (ArgumentNullException)
            {
                return 0.00;
            }
        }
        public static async Task<List<DetailedCryptoCurrency>> GetAssets()
        {
            HttpRequester requester = new HttpRequester();
            try
            {
                var response = await requester.GetRequest($"https://api.coincap.io/v2/assets");
                return JsonConvert.DeserializeObject<List<DetailedCryptoCurrency>>(response["data"].ToString());
            }
            catch (HttpRequestException)
            {
                return new List<DetailedCryptoCurrency>();
            }
            catch (ArgumentNullException)
            {
                return new List<DetailedCryptoCurrency>();
            }
        }

        public override string ToString()
        {
            return Id;
        }
    }
}
