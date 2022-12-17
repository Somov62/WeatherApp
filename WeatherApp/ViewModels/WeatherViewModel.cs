using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Mvvm;
using WeatherProvider;

namespace WeatherApp.ViewModels
{
    internal class WeatherViewModel : Base.BaseViewModel
    {
        public RelayCommand RefreshDataCommand { get; }

        public WeatherViewModel()
        {
            Title = "WeatherPage";

            RefreshDataCommand = new RelayCommand(async(o) => await RefreshData());
        }

        private WeatherForecast _forecast;
        public WeatherForecast Forecast
        {
            get => _forecast;
            set => Set(ref _forecast, value, nameof(Forecast));
        }


        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => Set(ref _isRefreshing, value, nameof(IsRefreshing));
        }

        private DayForecastModel _selectedForecast;

        public DayForecastModel SelectedForecast
        {
            get => _selectedForecast;
            set => Set(ref _selectedForecast, value, nameof(SelectedForecast));
        }



        private async Task RefreshData()
        {
            IsRefreshing = true;
            Forecast = await WeatherService.GetWeatherAsync(54.6f, 39.7f);
            SelectedForecast = Forecast.DayForecasts.Where(p => p.Date == DateTime.Now.Date).FirstOrDefault()!;
            IsRefreshing = false;
        }


    }
}
