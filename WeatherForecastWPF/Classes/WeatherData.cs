using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WeatherForecastWPF.Classes
{
	public class WeatherData
	{
		public static List<WeatherJsonParser> Weather = new List<WeatherJsonParser>();

		public string ApiKey = "GaEDdqRrmwA3VAqEEpNoHKGWpJKEtvRv";
		public string LanguageKey = "ru";
		public int PermLocationKey = 294922;
		public int MoscowLocationKey = 294021;
		public async Task<string> GetWeatherData(int LocationKey)
		{
			// URL строка запроса
			string requestUrl = $"http://dataservice.accuweather.com/forecasts/v1/daily/1day/294922" +
					$"?apikey={ApiKey}" +
					$"&language={LanguageKey}" +
					$"&metric=true" +
					$"&details=true";
			// Ответ API
			var responseApi = await SendWeatherRequest(requestUrl, LocationKey);

			// Возвращение данных
			return responseApi.Content.ReadAsStringAsync().Result;
		}
		private async Task<HttpResponseMessage> SendWeatherRequest(string Url, int LocationKey)
		{
			// HttpClient
			HttpClient httpClient = new HttpClient();
			// Параметры запроса
			var request = new HttpRequestMessage()
			{
				RequestUri = new System.Uri(Url),
				Method = HttpMethod.Get
			};
			// Получение данных
			var requestData = httpClient.SendAsync(request);
			// Ожидание получения данных
			while (requestData.Result == null)
				Task.WaitAll();
			// Возвращение данных
			return await requestData;
		}
        public static void ParseWeatherJson(string json)
        {
            try
            {
				// Отчистка списка с прогнозом погоды
				Weather.Clear();
                // Парсинг JSON
                var jsonObject = JObject.Parse(json);
                var dailyForecast = jsonObject["DailyForecasts"]?[0];

                if (dailyForecast != null)
                {
                    // Извлечение данных для дня
                    Console.WriteLine("Данные для дня:");
                    Weather.Add(new WeatherJsonParser(dailyForecast["Day"], "Пермь", "День"));

                    // Извлечение данных для ночи
                    Console.WriteLine("\nДанные для ночи:");
                    Weather.Add(new WeatherJsonParser(dailyForecast["Night"], "Пермь", "День"));

                    Debug.Listeners.Add(new TextWriterTraceListener("log.txt"));
                    foreach (WeatherJsonParser i in Weather)
                    {
                        Debug.WriteLine(i.Cloudy);
                        Debug.Flush();
                    }
                }
                else
                {
                    Console.WriteLine("Прогноз не найден в JSON-файле.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке JSON: {ex.Message}");
            }
        }
    }
}
