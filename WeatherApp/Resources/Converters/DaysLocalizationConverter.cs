using System;
using System.Globalization;
using System.Windows.Data;

namespace WeatherApp.Resources.Converters
{
    public class DaysLocalizationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            if (value is DateTime date)
            {
                return App.Current.Resources[date.Date.DayOfWeek.ToString()];
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
