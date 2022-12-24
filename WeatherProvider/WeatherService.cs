using Interfaces;
using Models;
using OpenMeteo;

namespace WeatherProvider
{
    public static class WeatherService
    {
        private static IWeatherProvider _weatherProvider = null!;

        public static void UseOpenMeteo()
        {
            if (_weatherProvider == null || !_weatherProvider.GetType().IsAssignableFrom(typeof(OpenMeteoProvider))) 
                _weatherProvider = new OpenMeteoProvider();
        }

        public static string ProviderDomain => _weatherProvider.ProviderDomain;

        public static ForecastMeasuresModel MeasureConfiguration { get; } = new ForecastMeasuresModel()
        {
            PrecipitationSum = LenghtMeasure.Mm,
            Rain = LenghtMeasure.Mm, 
            Showers = LenghtMeasure.Mm, 
            Snowfall = LenghtMeasure.Cm, 
            Temperature = TemperatureMeasure.Celsius,
            Windspeed = WindSpeed.Kmh
        };
        

        public static async Task<WeatherForecast>? GetWeatherAsync(float latitude, float longitude, string name = null)
        {
            //check cache
            try
            {
                var model = await _weatherProvider.GetWeatherAsync(latitude, longitude, MeasureConfiguration);
                model.Location.Name = name;
                return model;
            }
            catch (Exception)
            {
                return null;
            }
            //save in cache
        }

    }
}