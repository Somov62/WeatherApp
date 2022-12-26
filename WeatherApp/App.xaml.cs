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
            InitializeComponent();
            WeatherService.UseOpenMeteo();
            WeatherService.MeasureConfiguration.Windspeed = SettingsService.Configuration.Wind;
            WeatherService.MeasureConfiguration.Pressure = SettingsService.Configuration.Pressure;
            WeatherService.MeasureConfiguration.PrecipitationSum = SettingsService.Configuration.Lenght;
            WeatherService.MeasureConfiguration.Temperature = SettingsService.Configuration.Temperature;
            ServiceManager.InternetConnectionService.HostName = WeatherService.ProviderDomain;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            SettingsService.SaveChanges();
            base.OnExit(e);
        }

    }
}
