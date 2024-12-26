using System;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using WeatherForecastWPF.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using WeatherForecastWPF.Elements;
using System.Text.Json;

namespace WeatherForecastWPF
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static int Period = 5;
		public static int LocationKey = -1;
        public bool IsNeedApi = false;
		List<List<WeatherJsonParser>> AllPeriodWeatherJsonParser = new List<List<WeatherJsonParser>>();
		public MainWindow()
		{
			InitializeComponent();
		}
		private void ShowForecast(object sender, RoutedEventArgs e)
		{
			WeatherData weatherDataClient = new WeatherData();
            string filePath = "";

            LocationKey = CityCB.SelectedIndex == 0 ? WeatherData.PermLocationKey : WeatherData.MoscowLocationKey;
            Period = PeriodCB.SelectedIndex == 0 ? 1 : 5;

            // Если срок последнего обращения не найден или превышает 30 минут, то нужно обратиться к API
			if (File.ReadAllText("../../Files/LastApiRequestTime.txt") == String.Empty
				|| (DateTime.Now.Subtract((DateTime.Parse(File.ReadAllText("../../Files/LastApiRequestTime.txt")))) > TimeSpan.FromMinutes(30)))
			{
                IsNeedApi = true;
            }

            if (Period == 1 && LocationKey == WeatherData.PermLocationKey) filePath = "../../Files/SavedApiJsonOneDaysPerm.txt";
            else if (Period == 1 && LocationKey == WeatherData.MoscowLocationKey) filePath = "../../Files/SavedApiJsonOneDaysMoscow.txt";
            else if (Period == 5 && LocationKey == WeatherData.PermLocationKey) filePath = "../../Files/SavedApiJsonFiveDaysPerm.txt";
            else if (Period == 5 && LocationKey == WeatherData.MoscowLocationKey) filePath = "../../Files/SavedApiJsonFiveDaysMoscow.txt";

            // Если сохраненных данные не оказалось, то нужно обратиться к API
            if (File.ReadAllText(filePath) == String.Empty)
            {
                IsNeedApi = true;
            }
            
            if (IsNeedApi)
            {
                IsNeedApi = !IsNeedApi;
                Task<string> rawApiData = null;

                switch (Period)
                {
                    case 1: rawApiData = weatherDataClient.GetWeatherData(WeatherData.OneDayKey, LocationKey); break;
                    case 5: rawApiData = weatherDataClient.GetWeatherData(WeatherData.FiveDayKey, LocationKey); break;
                }

                StreamWriter streamWriter = new StreamWriter(filePath);
                StreamWriter streamWriterLastTimeRequest = new StreamWriter("../../Files/LastApiRequestTime.txt");

                // Чтение данных
                var normalizeApiData = rawApiData.Result.ToString();

                // Сохранение в файл
                streamWriter.WriteLine(normalizeApiData);
                streamWriterLastTimeRequest.WriteLine(DateTime.Now);

                streamWriter.Close();
                streamWriterLastTimeRequest.Close();
            }

            // Создание эксземпляра класса с фактическими параметрами
            string jsonNormalizedApiData = File.ReadAllText(filePath);
            WeatherJsonParser.ParseWeatherJson(jsonNormalizedApiData, ref AllPeriodWeatherJsonParser);

            Parent.Children.Clear();

            foreach (List<WeatherJsonParser> list in AllPeriodWeatherJsonParser)
            {
                Parent.Children.Add(new DayElement(list));
            }
        }
	}
}
