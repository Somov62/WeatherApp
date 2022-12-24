using System.Windows;
using WeatherApp.Services;
using WeatherProvider;
using Models;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            WeatherService.UseOpenMeteo();
            WeatherService.MeasureConfiguration.Windspeed = WindSpeed.Ms;
            WeatherService.MeasureConfiguration.Pressure = PressureMeasure.MmHg;
            ServiceManager.InternetConnectionService.HostName = WeatherService.ProviderDomain;
            System.Console.WriteLine();
        }

    }
}
