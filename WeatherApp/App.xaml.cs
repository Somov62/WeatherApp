using System.Windows;
using WeatherApp.Services;
using WeatherProvider;

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
            ServiceManager.InternetConnectionService.HostName = WeatherService.ProviderDomain;
        }

    }
}
