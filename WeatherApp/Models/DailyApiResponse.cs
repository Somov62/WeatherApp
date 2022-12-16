namespace WeatherApp.Models
{

    public class DailyApiResponse 
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Generationtime_ms { get; set; }
        public int Utc_offset_seconds { get; set; }
        public string? Timezone { get; set; }
        public string? Timezone_abbreviation { get; set; }
        public float Elevation { get; set; }
        public Metrics? Daily_units { get; set; }
        public DailyResponseBody? Daily { get; set; }
    }

    public class Metrics
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
        public string[]? Time { get; set; }
        public float[]? Temperature_2m_max { get; set; }
        public float[]? Temperature_2m_min { get; set; }
        public float[]? Precipitation_sum { get; set; }
        public float[]? Rain_sum { get; set; }
        public float[]? Showers_sum { get; set; }
        public float[]? Snowfall_sum { get; set; }
        public float[]? Precipitation_hours { get; set; }
        public int[]? Weathercode { get; set; }
        public string[]? Sunrise { get; set; }
        public string[]? Sunset { get; set; }
        public float[]? Windspeed_10m_max { get; set; }
        public float[]? Windgusts_10m_max { get; set; }
        public int[]? Winddirection_10m_dominant { get; set; }
    }

}
