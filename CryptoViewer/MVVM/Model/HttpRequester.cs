using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Windows.Controls;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace CryptoViewer.MVVM.Model
{
    public class HttpRequester
    {
        private readonly HttpClient _httpClient;
        private static readonly HttpRequester _instance = new HttpRequester();

        public static HttpRequester CurrentRequestor { get => _instance; }
        private HttpRequester()
        {
            _httpClient = new HttpClient();
        }

        public async Task<JObject> GetRequest(string url)
        {   
            if(url == null || url.Length == 0)
            {
                throw new ArgumentNullException("url");
            }

            using (_httpClient)
            {
                var response =  await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    return (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());    
                }

                else
                {
                    throw new HttpRequestException($"Bad request. Server error code {response.StatusCode}");
                }

            }
        }
    }
}
