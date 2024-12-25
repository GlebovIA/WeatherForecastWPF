using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherForecastWPF.Classes
{
	public class WeatherData
	{
		public string ApiKey = "tDZyrBCCajFnMNTpoOSPtaVlAAEDL8Zb";
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
	}
}
