using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Linq;

namespace WeatherForecastWPF.Classes
{
	public class WeatherJsonParser
	{
        public string Sity {  get; set; }
        public string PeriodName { get; set; }
        public double? Temperature { get; set; }
        public double? FeelTemperature { get; set; }
        public string Cloudy {  get; set; }
        public double? WindSpeed { get; set; }
        public string WindDirection { get; set; }
        public double? Humidity { get; set; }
        public string UVIndex { get; set; }
		public WeatherJsonParser(JToken periodData, string sity, string periodName)
		{
            if (periodData == null)
            {
                Console.WriteLine("Данные не найдены.");
                return;
            }
            Sity = sity;

            PeriodName = periodName;

            Temperature = periodData["Temperature"]?["Average"]?["Value"]?.ToObject<double>();

            // Извлечение ощущаемой температуры
            FeelTemperature = periodData["RealFeelTemperatureShade"]?["Value"]?.ToObject<double>(); 

            // Извлечение облачности
            Cloudy = periodData["IconPhrase"]?.ToObject<string>();

            // Извлечение скорости и направления ветра
            WindSpeed = periodData["Wind"]?["Speed"]?["Value"]?.ToObject<double>();
            WindDirection = periodData["Wind"]?["Direction"]?["Localized"]?.ToString();

            // Извлечение влажности
            Humidity = periodData["RelativeHumidity"]?["Average"].ToObject<double>();

            // Извлечение УФ-индекса
            UVIndex = periodData["AirAndPollen"]?.FirstOrDefault(p => p["Name"]?.ToString() == "UVIndexText")?["Value"]?.ToObject<string>();
        }
    }
}
