using System;
using System.Globalization;
using System.Windows.Data;
using Models;

namespace WeatherApp.Resources.Converters
{
    public class MeasuresConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            if (value is WindSpeed windSpeed)
            {
                return App.Current.Resources[windSpeed.ToString()];
            }

            if (value is LenghtMeasure lenght)
            {
                return App.Current.Resources[lenght.ToString()];
            }

            if (value is TemperatureMeasure temperature)
            {
                return App.Current.Resources[temperature.ToString()];
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
