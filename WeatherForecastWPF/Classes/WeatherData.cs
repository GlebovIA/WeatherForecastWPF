using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherForecastWPF.Classes
{
	public class WeatherData
	{
		private string ApiKey = "GaEDdqRrmwA3VAqEEpNoHKGWpJKEtvRv";
		private string LanguageKey = "ru";
		public static string OneDayKey = "1day";
		public static string FiveDayKey = "5day";
		public static int PermLocationKey = 294922;
		public static int MoscowLocationKey = 294021;
		public async Task<string> GetWeatherData(string DayKey, int LocationKey)
		{
			// URL строка запроса
			string requestUrl = $"http://dataservice.accuweather.com/forecasts/v1/daily" +
				$"/{DayKey}" +
				$"/{LocationKey}" +
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
			Debug.WriteLine("Подтянул с API");

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

	}
}
