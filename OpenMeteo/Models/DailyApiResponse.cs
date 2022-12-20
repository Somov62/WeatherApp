namespace WeatherApp.Models
{

    public class DailyApiResponse 
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int Utc_offset_seconds { get; set; }
        public string? Timezone { get; set; }
        public string? Timezone_abbreviation { get; set; }
        public float Elevation { get; set; }
        public Measures? Daily_units { get; set; }
        public DailyResponseBody? Daily { get; set; }
        public HourlyResponseBody? Hourly { get; set; }
    }

    public class Measures
    {
        public string? Time { get; set; }
        public string? Temperature_2m_max { get; set; }
        public string? Temperature_2m_min { get; set; }
        public string? Rain_sum { get; set; }
        public string? Showers_sum { get; set; }
        public string? Snowfall_sum { get; set; }
        public string? Precipitation_sum { get; set; }
        public string? Precipitation_hours { get; set; }
        public string? Weathercode { get; set; }
        public string? Sunrise { get; set; }
        public string? Sunset { get; set; }
        public string? Windspeed_10m_max { get; set; }
        public string? Windgusts_10m_max { get; set; }
        public string? Winddirection_10m_dominant { get; set; }
    }

    public class DailyResponseBody
    {
        public List<DateTime>? Time { get; set; }
        public List<float>? Temperature_2m_max { get; set; }
        public List<float>? Temperature_2m_min { get; set; }
        public List<float>? Precipitation_sum { get; set; }
        public List<float>? Rain_sum { get; set; }
        public List<float>? Showers_sum { get; set; }
        public List<float>? Snowfall_sum { get; set; }
        public List<float>? Precipitation_hours { get; set; }
        public List<int>? Weathercode { get; set; }
        public List<DateTime>? Sunrise { get; set; }
        public List<DateTime>? Sunset { get; set; }
        public List<float>? Windspeed_10m_max { get; set; }
        public List<float>? Windgusts_10m_max { get; set; }
        public List<int>? Winddirection_10m_dominant { get; set; }
    }

    public class HourlyResponseBody
    {
        public List<DateTime>? Time { get; set; }
        public List<float>? Temperature_2m { get; set; }
        public List<float>? Relativehumidity_2m { get; set; }
        public List<float>? Apparent_temperature { get; set; }
        public List<float>? Surface_pressure { get; set; }
        public List<float>? Windspeed_10m { get; set; }
        public List<int>? Winddirection_10m { get; set; }
        public List<int>? Weathercode { get; set; }
    }
}
