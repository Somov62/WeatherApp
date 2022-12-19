using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WeatherApp.Resources.Converters
{
    public class WeekendHighlightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return (SolidColorBrush)App.Current.Resources["PageForeground"];

            if (value is DateTime date)
            {
                if (date.Date.DayOfWeek == DayOfWeek.Sunday || date.Date.DayOfWeek == DayOfWeek.Saturday)
                    return (SolidColorBrush)App.Current.Resources["WeekendHighlight"];
            }
            return (SolidColorBrush)App.Current.Resources["PageForeground"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
