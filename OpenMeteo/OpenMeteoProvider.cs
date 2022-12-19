using Interfaces;
using Models;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using WeatherApp.Models;

namespace OpenMeteo
{
    public class OpenMeteoProvider : IWeatherProvider
    {
        private readonly HttpClient _httpClient;

        public string ProviderDomain { get; } = "open-meteo.com";


        public OpenMeteoProvider()
        {
            _httpClient = new() { BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast/") };
        }

        public async Task<WeatherForecast> GetWeatherAsync(float latitude, float longitude, ForecastMeasuresModel measures)
        {
            StringBuilder url = new ();
            url.Append("?latitude=" + latitude.ToString(CultureInfo.InvariantCulture));
            url.Append("&longitude=" + longitude.ToString(CultureInfo.InvariantCulture));
            url.Append("&temperature_unit=" + measures.Temperature.ToString().ToLower());
            url.Append("&windspeed_unit=" + measures.Windspeed.ToString().ToLower());
            url.Append("&precipitation_unit=" + measures.PrecipitationSum.ToString().ToLower());
            url.Append("&latitude=" + latitude.ToString(CultureInfo.InvariantCulture));
            url.Append("&timezone=auto");
            url.Append("&past_days=2");
            url.Append("&daily=temperature_2m_max,temperature_2m_min,precipitation_sum,rain_sum,showers_sum,snowfall_sum,precipitation_hours,weathercode,sunrise,sunset,windspeed_10m_max,windgusts_10m_max,winddirection_10m_dominant");

            DailyApiResponse response;
            try
            {
                response = await _httpClient.GetFromJsonAsync<DailyApiResponse>(url.ToString());
            }
            catch (Exception ex)
            {
                return null!;
            }

            return ToUserModel(response!, measures);
        }

        private WeatherForecast ToUserModel(DailyApiResponse apiModel, ForecastMeasuresModel measures)
        {
            WeatherForecast userModel = new ();

            userModel.Location = new()
            {
                Latitude = apiModel.Latitude,
                Longitude = apiModel.Longitude,
                Elevation = apiModel.Elevation,
                Timezone = apiModel.Timezone,
                TimezoneAbbreviation = apiModel.Timezone_abbreviation,
                UtcOffsetSeconds = apiModel.Utc_offset_seconds,
            };

            userModel.Measures = measures;

            userModel.DayForecasts = new List<DayForecastModel>();
            for (int i = 0; i < apiModel?.Daily?.Time?.Count; i++)
            {
                userModel.DayForecasts.Add(
                    new DayForecastModel()
                    {
                        Date = apiModel.Daily.Time[i],
                        Weather = (WeatherCodes)apiModel.Daily.Weathercode[i],
                        MaxTemperature = apiModel.Daily.Temperature_2m_max[i],
                        MinTemperature = apiModel.Daily.Temperature_2m_min[i],
                        RainSum = apiModel.Daily.Rain_sum[i],
                        ShowersSum = apiModel.Daily.Showers_sum[i],
                        SnowfallSum = apiModel.Daily.Snowfall_sum[i],
                        SumPrecipitation = apiModel.Daily.Precipitation_sum[i],
                        Sunrise = apiModel.Daily.Sunrise[i],
                        Sunset = apiModel.Daily.Sunset[i],
                        WindDirection = apiModel.Daily.Winddirection_10m_dominant[i],
                        WindGusts = apiModel.Daily.Windgusts_10m_max[i],
                        WindSpeed = apiModel.Daily.Windspeed_10m_max[i]
                    });
            }

            userModel.StartDate = apiModel.Daily.Time.First();
            userModel.EndDate = apiModel.Daily.Time.Last();

            return userModel;
        }
    }
}