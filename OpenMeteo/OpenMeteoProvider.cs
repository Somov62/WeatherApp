using Interfaces;
using System.Net;

namespace OpenMeteo
{
    public class OpenMeteoProvider : IWeatherProvider
    {
        private readonly HttpClient _httpClient;

        public string ProviderDomain { get; } = "open-meteo.com";


        public OpenMeteoProvider()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast/");
        }

        public async void GetWeather(double longitude, double latitude)
        {
            await RequestWeatherData(longitude, latitude);
        }

        private async Task<string> RequestWeatherData(double longitude, double latitude)
        {
            var response = await _httpClient.GetAsync($"https://api.open-meteo.com/v1/forecast?longitude={longitude}&latitude={latitude}&daily=temperature_2m_max,temperature_2m_min,precipitation_sum,rain_sum,showers_sum,snowfall_sum,precipitation_hours,weathercode,sunrise,sunset,windspeed_10m_max,windgusts_10m_max,winddirection_10m_dominant&timezone=Europe/Moscow&past_days=2");
            var str = response.Content.ToString();
            return str;
        }

    }
}