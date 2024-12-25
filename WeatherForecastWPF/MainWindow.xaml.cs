using System;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using WeatherForecastWPF.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using WeatherForecastWPF.Elements;

namespace WeatherForecastWPF
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static int Period = 5;
		public static int LocationKey = -1;
		List<List<WeatherJsonParser>> AllPeriodWeatherJsonParser = new List<List<WeatherJsonParser>>();
		public MainWindow()
		{
			InitializeComponent();
		}
		private void ShowForecast(object sender, RoutedEventArgs e)
		{
			WeatherData weatherDataClient = new WeatherData();
			// Если содержаться все данные, то обращаться к API не нужно (Ограничение на 50 запросов в день)
			if (File.ReadAllText("../../Files/SavedApiJson.txt") == String.Empty)
			{
				Task<string> rawApiData = null;

				LocationKey = CityCB.SelectedIndex == 0 ? WeatherData.PermLocationKey : WeatherData.MoscowLocationKey;

				switch (Period)
				{
					case 1: rawApiData = weatherDataClient.GetWeatherData(WeatherData.OneDayKey, LocationKey); break;
					case 5: rawApiData = weatherDataClient.GetWeatherData(WeatherData.FiveDayKey, LocationKey); break;
				}

				StreamWriter streamWriter = new StreamWriter("../../Files/SavedApiJson.txt");

				// Чтение данных
				var normalizeApiData = rawApiData.Result.ToString();

				// Сохранение в файл
				streamWriter.WriteLine(normalizeApiData);
				streamWriter.Close();
			}
			// Создание эксземпляра класса с фактическими параметрами
			string jsonNormalizedApiData = File.ReadAllText("../../Files/SavedApiJson.txt");
			WeatherJsonParser.ParseWeatherJson(jsonNormalizedApiData, ref AllPeriodWeatherJsonParser);

			foreach(List<WeatherJsonParser> list in AllPeriodWeatherJsonParser)
			{
				Parent.Children.Add(new DayElement(list));
			}
		}
	}
}
