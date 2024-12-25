using System.Windows;
using WeatherForecastWPF.Classes;
using System.IO;
using System;
using System.Diagnostics;

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
			if (File.ReadAllText("../../Files/SavedApiJson.txt") == String.Empty)
			{
				var rawApiData = weatherDataClient.GetWeatherData(294922);
				var normalizeApiData = rawApiData.Result.ToString();
                // Сохранение в файл
				StreamWriter streamWriter = new StreamWriter(".. / .. / Files / SavedApiJson.txt");
				streamWriter.WriteLine(normalizeApiData);
				streamWriter.Close();
			}
			// Создание эксземпляра класса с фактическими параметрами
			string jsonNormalizedApiData= File.ReadAllText("../../Files/SavedApiJson.txt");
			WeatherData.ParseWeatherJson(jsonNormalizedApiData);
        }
	}
}
