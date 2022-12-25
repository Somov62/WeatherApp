using Models;
using WeatherApp.Services;
using WeatherProvider;

namespace WeatherApp.ViewModels
{
    internal class SettingsViewModel : Base.BaseViewModel
    {
        private Configuration _config = SettingsService.Configuration;
        public SettingsViewModel()
        {
            Title = App.Current.Resources["SettingsPageTitle"].ToString()!;
            _tempController = _config.Temperature == TemperatureMeasure.Fahrenheit;
            _windController = _config.Wind == WindSpeed.Kmh;
            _pressureController = _config.Pressure == PressureMeasure.MmHg;
            _precipitationController = _config.Lenght == LenghtMeasure.Inch;
        }


        private bool _tempController;

        public bool TempController
        {
            get => _tempController;
            set 
            { 
                Set(ref _tempController, value, nameof(TempController));
                if (value) _config.Temperature = TemperatureMeasure.Fahrenheit;
                else _config.Temperature = TemperatureMeasure.Celsius;
                WeatherService.MeasureConfiguration.Temperature = _config.Temperature;
            }
        }


        private bool _precipitationController;

        public bool PrecipitationController
        {
            get => _precipitationController;
            set
            {
                Set(ref _precipitationController, value, nameof(PrecipitationController));
                if (value) _config.Lenght = LenghtMeasure.Inch;
                else _config.Lenght = LenghtMeasure.Mm;
                WeatherService.MeasureConfiguration.PrecipitationSum = _config.Lenght;
            }
        }


        private bool _windController;

        public bool WindController
        {
            get => _windController;
            set
            {
                Set(ref _windController, value, nameof(WindController));
                if (value) _config.Wind = WindSpeed.Kmh;
                else _config.Wind = WindSpeed.Ms;
                WeatherService.MeasureConfiguration.Windspeed = _config.Wind;

            }
        }

        private bool _pressureController;

        public bool PressureController
        {
            get => _pressureController;
            set
            {
                Set(ref _pressureController, value, nameof(PressureController));
                if (value) _config.Pressure = PressureMeasure.MmHg;
                else _config.Pressure = PressureMeasure.HPa;
                WeatherService.MeasureConfiguration.Pressure = _config.Pressure;
            }
        }
    }
}
