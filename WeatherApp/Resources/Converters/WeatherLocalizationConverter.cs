using Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace WeatherApp.Resources.Converters
{
    public class WeatherLocalizationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return App.Current.Resources["NoData"];

            if (value is WeatherCodes weather)
            {
                return App.Current.Resources[weather.ToString()];
            }
            return App.Current.Resources["NoData"];

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
