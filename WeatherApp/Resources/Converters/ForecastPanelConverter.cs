using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using WeatherApp.ViewModels;

namespace WeatherApp.Resources.Converters
{
    public class ForecastPanelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vm = value as WeatherViewModel;
            if (vm != null) return false;
            var day = (parameter as Grid).DataContext as DayForecastModel;
            return vm.SelectedForecast.Equals(day);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
