using Interfaces;
using OpenMeteo;
using System.Windows;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            IWeatherProvider weather = new OpenMeteoProvider();

            weather.GetWeather(54.6, 39.7);
        }

    }
}
