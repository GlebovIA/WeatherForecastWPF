using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherForecastWPF.Classes
{
    public static class WeatherData
    {
        public static string ApiKey = "tDZyrBCCajFnMNTpoOSPtaVlAAEDL8Zb";
        public static string LanguageKey = "ru";
        public static int PermLocationKey = 294922;
        public static int MoscowLocationKey = 294021;
        public static async Task<object> GetWeatherDataAsync(int LocationKey)
        {
            string requestUrl = $"http://dataservice.accuweather.com/forecasts/v1/daily/1day/294922" +
                $"?apikey={ApiKey}" +
                $"&language={LanguageKey}";

            var response = await SendWeatherRequestAsync(requestUrl, LocationKey);

            return response;
        }
        private static async Task<object> SendWeatherRequestAsync(string Url, int LocationKey)
        {
            using (HttpClient httpClient = new HttpClient())
            {

                var request = new HttpRequestMessage()
                {
                    RequestUri = new System.Uri(Url),
                    Method = HttpMethod.Get
                };

                return await httpClient.SendAsync(request);
            }
        }
    }
}
