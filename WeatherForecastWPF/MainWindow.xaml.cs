using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Windows;
using WeatherForecastWPF.Classes;

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

			var rawApiData = weatherDataClient.GetWeatherData(294922);
			var normalizeApiData = JsonSerializer.Serialize(rawApiData.Result.ToString());
			var test = 294922;
		}
	}
}
