using Models;
using System;
using System.Globalization;
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
            var day = (parameter as Grid).DataContext as DayForecastModel;
            if (vm == null || day == null) return false;
            return vm.SelectedForecast.Equals(day);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
