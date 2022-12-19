using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeatherApp.Resources.Converters
{
    public class WindConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return 0;
            var angle = (int)value;
            return 180 - angle;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class WindToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;
            var angle = (int)value;

            if ((angle > 0 && angle < 22.5) || angle > 337.5)
                return App.Current.Resources["East"].ToString();

            if (angle >= 22.5 && angle <= 67.5)
                return App.Current.Resources["North"].ToString() + App.Current.Resources["East"].ToString();

            if (angle > 67.5 && angle < 112.5)
                return App.Current.Resources["North"].ToString();

            if (angle >= 112.5 && angle <= 157.5)
                return App.Current.Resources["North"].ToString() + App.Current.Resources["West"].ToString();

            if (angle > 157.5 && angle < 202.5)
                return App.Current.Resources["West"].ToString();

            if (angle >= 202.5 && angle <= 247.5)
                return App.Current.Resources["South"].ToString() + App.Current.Resources["West"].ToString();

            if (angle > 247.5 && angle < 292.5)
                return App.Current.Resources["South"].ToString();

            if (angle >= 292.5 && angle <= 337.5)
                return App.Current.Resources["North"].ToString() + App.Current.Resources["East"].ToString();
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
