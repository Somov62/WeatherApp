using System;
using System.Collections.Generic;

namespace Models
{
    public class WeatherForecast
    {
        public LocationModel Location { get; set; } = null!;
        public ForecastMeasuresModel Measures { get; set; } = null!;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<DayForecastModel> DayForecasts { get; set; } = new List<DayForecastModel>();
    }

    public class DayForecastModel
    {
        public DateTime Date { get; set; }

        public float MaxTemperature { get; set; }
        public float MinTemperature { get; set; }

        public float SumPrecipitation { get; set; }
        public float RainSum { get; set; }
        public float ShowersSum { get; set; }
        public float SnowfallSum { get; set; }


        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }
        public TimeSpan DayTime { get; set; }

        public float Pressure { get; set; }

        public float WindSpeed { get; set; }
        public float WindGusts { get; set; }
        public int WindDirection { get; set; }

        public WeatherCodes Weather  { get; set; }

        public List<HourlyForecastModel> HourlyForecasts { get; set; } = new List<HourlyForecastModel>();
    }

    public class HourlyForecastModel
    {
        public DateTime Time { get; set; }

        public float Temperature { get; set; }
        public float ApparentTemperature { get; set; }
        public float RelativeHumidity { get; set; }
        public float SurfasePressure { get; set; }
        public float WindSpeed { get; set; }
        public int WindDirection { get; set; }
        public WeatherCodes Weather { get; set; }
    }

    public class LocationModel
    {
        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public int UtcOffsetSeconds { get; set; }

        public string? Timezone { get; set; }

        public string? TimezoneAbbreviation { get; set; }

        public float Elevation { get; set; }
    }


    public class ForecastMeasuresModel
    {
        public TemperatureMeasure Temperature { get; set; }

        public LenghtMeasure PrecipitationSum { get; set; }

        public LenghtMeasure Rain { get; set; }
        public LenghtMeasure Showers { get; set; }
        public LenghtMeasure Snowfall { get; set; }

        public WindSpeed Windspeed { get; set; }
        public PressureMeasure Pressure { get; set; }
    }
}
