using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using Models;

namespace WeatherApp.Resources.Converters
{
    public class WeatherCodeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null!;
            DateTime dateTime;
            if (parameter == null)
                dateTime = DateTime.Now;
            else dateTime = (DateTime)parameter;

            string resourceName;
            switch ((WeatherCodes)value)
            {
                case WeatherCodes.ClearSky:
                    if (IsDayTime(dateTime))
                        resourceName = "clear-day";
                    else resourceName = "clear-night";
                    break;
                case WeatherCodes.MainlyClear:
                    if (IsDayTime(dateTime))
                        resourceName = "party-cloudly-day";
                    else resourceName = "party-cloudly-night";
                    break;
                case WeatherCodes.PartlyCloudly:
                    resourceName = "cloudly";
                    break;
                case WeatherCodes.Overcast:
                    resourceName = "overcast";
                    break;

                case WeatherCodes.Fog:
                case WeatherCodes.DepositingRimeFog:
                    resourceName = "fog";
                    break;

                case WeatherCodes.LightDrizzle:
                case WeatherCodes.ModerateDrizzle:
                case WeatherCodes.DenseDrizzle:
                    resourceName = "drizzle";
                    break;

                case WeatherCodes.LightFreezingDrizzle:
                case WeatherCodes.DenseFreezingDrizzle:
                case WeatherCodes.SlightSnowShowers:
                case WeatherCodes.HeavySnowShowers:
                    resourceName = "freezing-drizzle";
                    break;
                case WeatherCodes.SlightRain:
                case WeatherCodes.ModerateRain:
                case WeatherCodes.HeavyRain:
                    resourceName = "rain";
                    break;

                case WeatherCodes.SlightSnowfall:
                    resourceName = "slight-snowfall";
                    break;
                case WeatherCodes.ModerateSnowfall:
                case WeatherCodes.SnowGrains:
                    resourceName = "moderate-snowfall";
                    break;
                case WeatherCodes.HeavySnowfall:
                    resourceName = "hard-snowfall";
                    break;

                case WeatherCodes.SlightRainShowers:
                case WeatherCodes.ModerateRainShowers:
                case WeatherCodes.ViolentRainShowers:
                    resourceName = "shower";
                    break;

                case WeatherCodes.Thunderstorm:
                case WeatherCodes.ThunderstormWithSlightHail:
                case WeatherCodes.ThunderstormWithHeavyHail:
                    resourceName = "thundestorm";
                    break;

                default:
                    return null!;
            }

            return App.Current.Resources[resourceName] as ControlTemplate;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private bool IsDayTime(DateTime dateTime)
        {
            return dateTime.TimeOfDay > new TimeSpan(5, 0, 0) && dateTime.TimeOfDay < new TimeSpan(22, 0, 0);
        }
    }
}
