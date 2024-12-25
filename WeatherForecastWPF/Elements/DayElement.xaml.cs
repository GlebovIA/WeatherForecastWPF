using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeatherForecastWPF.Classes;

namespace WeatherForecastWPF.Elements
{
    /// <summary>
    /// Логика взаимодействия для DayElement.xaml
    /// </summary>
    public partial class DayElement : UserControl
    {
        public DayElement(List<WeatherJsonParser> Day)
        {
            InitializeComponent();
            DateL.Content = Day[0].PeriodDate;
            if (Day[0].PeriodDate == DateTime.Now.Date.ToString("yyyy-MM-dd"))
                TodayL.Content = "сегодня";
            UVIndexL.Content = Day[0].UVIndex;
            DayElementsSP.Children.Add(new DayPartWeatherElement(Day[0]));
            DayElementsSP.Children.Add(new DayPartWeatherElement(Day[1]));
        }
    }
}
