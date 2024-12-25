using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WeatherForecastWPF.Classes;

namespace WeatherForecastWPF.Elements
{
    /// <summary>
    /// Логика взаимодействия для DayPartWeatherElement.xaml
    /// </summary>
    public partial class DayPartWeatherElement : UserControl
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
            if (DayPart.Cloudy.Contains("блачно") || DayPart.Cloudy.Contains("асмурно"))
            {
                Image.Source = new BitmapImage(new System.Uri("../../Images/Cloud.png", System.UriKind.RelativeOrAbsolute));
            }
            else if (DayPart.Cloudy.Contains("ивни"))
            {
                Image.Source = new BitmapImage(new System.Uri("../../Images/Rain.png", System.UriKind.RelativeOrAbsolute));
            }
            else if (DayPart.Cloudy.Contains("ожд"))
            {
                Image.Source = new BitmapImage(new System.Uri("../../Images/Rain.png", System.UriKind.RelativeOrAbsolute));
            }
            else if (DayPart.Cloudy.Contains("нег"))
            {
                Image.Source = new BitmapImage(new System.Uri("../../Images/Snow.png", System.UriKind.RelativeOrAbsolute));
            }
            else if (DayPart.Cloudy.Contains("сно"))
            {
                Image.Source = new BitmapImage(new System.Uri("../../Images/Sun.png", System.UriKind.RelativeOrAbsolute));
            }
        }
    }
}
