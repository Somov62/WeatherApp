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
            url.Append("&hourly=temperature_2m,relativehumidity_2m,apparent_temperature,surface_pressure,windspeed_10m,winddirection_10m,weathercode");

            DailyApiResponse response;
            try
            {
                response = await _httpClient.GetFromJsonAsync<DailyApiResponse>(url.ToString());
            }
            catch
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

            int hoursCounter = 0;
            for (int i = 0; i < apiModel?.Daily?.Time?.Count; i++)
            {
                DayForecastModel day = new ()
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
                };
                float pressure = 0;
                for (int j = hoursCounter; j < hoursCounter + 24; j++)
                {
                    HourlyForecastModel hour = new ()
                    {
                        ApparentTemperature = apiModel.Hourly.Apparent_temperature[j],
                        RelativeHumidity = apiModel.Hourly.Relativehumidity_2m[j],
                        Temperature = apiModel.Hourly.Temperature_2m[j],
                        Time = apiModel.Hourly.Time[j],
                        Weather = (WeatherCodes)apiModel.Hourly.Weathercode[j],
                        WindDirection = apiModel.Hourly.Winddirection_10m[j],
                        WindSpeed = apiModel.Hourly.Windspeed_10m[j]
                    };
                    hour.SurfasePressure = apiModel.Hourly.Surface_pressure[j];
                    if (measures.Pressure == PressureMeasure.MmHg)
                        hour.SurfasePressure *= 0.7501f;
                    pressure += hour.SurfasePressure;
                    day.HourlyForecasts.Add(hour);
                }
                day.Pressure = pressure / 24;
                hoursCounter += 24;
                userModel.DayForecasts.Add(day);
                   
            }

            userModel.StartDate = apiModel.Daily.Time.First();
            userModel.EndDate = apiModel.Daily.Time.Last();

            return userModel;
        }
    }
}