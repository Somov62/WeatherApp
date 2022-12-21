using Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace WeatherApp.Resources.Converters
{
    public class MoonPhazesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null!;

            if (value is MoonPhazes phaze)
            {
                return App.Current.Resources[phaze.ToString()];
            }
            return null!;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
