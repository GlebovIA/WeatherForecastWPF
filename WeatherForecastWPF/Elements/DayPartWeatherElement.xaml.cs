using System.Windows.Controls;
using WeatherForecastWPF.Models;

namespace WeatherForecastWPF.Elements
{
    /// <summary>
    /// Логика взаимодействия для DayPartWeatherElement.xaml
    /// </summary>
    public partial class DayPartWeatherElement : Page
    {
        public DayPartWeatherElement(DayPart dayPart)
        {
            InitializeComponent();
            DayPartNameL.Content = dayPart.Name;
            TemperatureL.Content = dayPart.Temperature;
            WeatherL.Content = dayPart.Weather;
            TensionL.Content = dayPart.Tension;
            HumidityL.Content = dayPart.Humidity;
            WindL.Content = dayPart.Wind;
            FeelTemperatureL.Content = dayPart.FeelTemperature;
        }
    }
}
