using Models;

namespace Interfaces
{
    public interface IWeatherProvider
    {
        public string ProviderDomain { get; }

        public Task<WeatherForecast> GetWeatherAsync(float latitude, float longitude, ForecastMeasuresModel metrics);
    }
}
