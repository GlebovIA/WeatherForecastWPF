using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Documents;
using System.Collections.Generic;

namespace WeatherForecastWPF.Classes
{
	public class WeatherJsonParser
	{
		public string Sity { get; set; }
        public string PeriodDate { get; set; }
        public string PeriodName { get; set; }
		public double? Temperature { get; set; }
		public double? FeelTemperature { get; set; }
		public string Cloudy { get; set; }
		public double? WindSpeed { get; set; }
		public string WindDirection { get; set; }
		public double? Humidity { get; set; }
		public string UVIndex { get; set; }
		public WeatherJsonParser() { }
		public WeatherJsonParser(JToken periodData, string periodState, string sity, string periodName)
		{
			if (periodData == null)
			{
				Console.WriteLine("Данные не найдены.");
				return;
			}
			Sity = sity;

			PeriodDate = periodData["Date"].ToObject<string>();


            PeriodName = periodName;

			Temperature = periodData["Temperature"]?["Minimum"]?["Value"]?.ToObject<double>();

			// Извлечение ощущаемой температуры
			FeelTemperature = periodData["RealFeelTemperatureShade"]?["Minimum"]?["Value"]?.ToObject<double>();

			// Извлечение облачности
			Cloudy = periodData?[periodState]?["IconPhrase"]?.ToObject<string>();

			// Извлечение скорости и направления ветра
			WindSpeed = periodData?[periodState]?["Wind"]?["Speed"]?["Value"]?.ToObject<double>();
			WindDirection = periodData?[periodState]?["Wind"]?["Direction"]?["Localized"]?.ToString();

			// Извлечение влажности
			Humidity = periodData?[periodState]?["RelativeHumidity"]?["Average"].ToObject<double>();

			// Извлечение УФ-индекса
			UVIndex = periodData?["AirAndPollen"]?[5]?["Category"]?.ToObject<string>();
		}
		public static void ParseWeatherJson(string Json, ref List<List<WeatherJsonParser>> Weather)
		{
			try
			{
				Weather.Clear();
				// Парсинг JSON
				var jsonObject = JObject.Parse(Json);
				var dailyForecast = jsonObject["DailyForecasts"];

				if (dailyForecast != null)
				{
					foreach (var dayForecast in dailyForecast)
					{
						List<WeatherJsonParser> dailyJsonParser = new List<WeatherJsonParser>();
						// Извлечение данных для дня
						Console.WriteLine("Данные для дня:");
						dailyJsonParser.Add(new WeatherJsonParser(dayForecast, "Day", "Пермь", "День"));
						// Извлечение данных для ночи
						Console.WriteLine("\nДанные для ночи:");
						dailyJsonParser.Add(new WeatherJsonParser(dayForecast, "Night", "Пермь", "Ночь"));
						// Передача суточной информации
						Weather.Add(dailyJsonParser);
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
