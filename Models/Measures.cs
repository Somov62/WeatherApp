using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public enum WeatherCodes
    {
        ClearSky = 0,
        MainlyClear = 1,
        PartlyCloudly = 2,
        Overcast = 3,

        Fog = 45,
        DepositingRimeFog = 48,

        LightDrizzle = 51,
        ModerateDrizzle = 53,
        DenseDrizzle = 55,

        LightFreezingDrizzle = 56,
        DenseFreezingDrizzle = 57,

        SlightRain = 61,
        ModerateRain = 63,
        HeavyRain = 65,

        SlightSnowfall = 71,
        ModerateSnowfall = 73,
        HeavySnowfall = 75,
        SnowGrains = 77,

        SlightRainShowers = 80,
        ModerateRainShowers = 81,
        ViolentRainShowers = 82,

        SlightSnowShowers = 85,
        HeavySnowShowers = 86,

        Thunderstorm = 95,
        ThunderstormWithSlightHail = 96,
        ThunderstormWithHeavyHail = 99,
    }

    public enum TemperatureMeasure
    {
        Celsius,
        Fahrenheit
    }

    public enum WindSpeed
    {
        Kmh,
        Ms,
        Mph,
        Kn
    }

    public enum LenghtMeasure
    {
        Mm,
        Cm,
        Inch
    }

    public enum PressureMeasure
    {
        HPa,
        MmHg
    }
}
