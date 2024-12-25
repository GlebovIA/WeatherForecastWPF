using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecastWPF.Models
{
    public class DayPart
    {
        public string Name;
        public int Temperature;
        public string Weather;
        public int Tension;
        public int Humidity;
        public string Wind;
        public int WindSpeed;
        public int FeelTemperature;
        public DayPart(string name, int tempereature, string weather, int tension, int humidity, string wind, int windSpeed, int feelTemperature)
        {
            Name = name;
            Temperature = tempereature;
            Weather = weather;
            Temperature = tension;
            Humidity = humidity;
            Wind = wind;
            WindSpeed = windSpeed;
            FeelTemperature = feelTemperature;
        }
    }
}
