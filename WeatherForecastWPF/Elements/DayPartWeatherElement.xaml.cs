using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WeatherForecastWPF.Classes;

namespace WeatherForecastWPF.Elements
{
    /// <summary>
    /// Логика взаимодействия для DayPartWeatherElement.xaml
    /// </summary>
    public partial class DayPartWeatherElement : Page
    {
        public DayPartWeatherElement(WeatherJsonParser DayPart)
        {
            InitializeComponent();
            DayPartNameL.Content = DayPart.PeriodName;
            TemperatureL.Content = DayPart.Temperature;
            CloudyL.Content = DayPart.Cloudy;
            HumidityL.Content = DayPart.Humidity;
            WindL.Content = DayPart.WindSpeed + " " + DayPart.WindDirection;
            FeelTemperatureL.Content = DayPart.FeelTemperature;
            if (DayPart.Cloudy.Contains("облачно"))
            {
                Image.Source = new BitmapImage(new System.Uri("/WeatherForecastWPF;component/Images/Cloud.png"));
            }
        }
    }
}
