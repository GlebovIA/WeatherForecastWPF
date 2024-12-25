using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Windows;
using WeatherForecastWPF.Classes;
using WeatherForecastWPF;
using System.IO;
using System;

namespace WeatherForecastWPF
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			WeatherData weatherDataClient = new WeatherData();
			// Если содержаться данные, то обращаться к API не нужно (Ограничение в 50 запросов в день)
			if (File.ReadAllText(@"C:\Users\user\Desktop\Weather\WeatherForecastWPF\Files\SavedApiJson.txt") == String.Empty)
			{
				var rawApiData = weatherDataClient.GetWeatherData(294922);
				var normalizeApiData = rawApiData.Result.ToString();

				StreamWriter streamWriter = new StreamWriter(@"C:\Users\user\Desktop\Weather\WeatherForecastWPF\Files\SavedApiJson.txt");
				streamWriter.Write(normalizeApiData);
			}

		}
	}
}
