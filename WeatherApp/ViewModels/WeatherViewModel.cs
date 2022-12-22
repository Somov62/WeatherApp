using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Models;
using SkiaSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Mvvm;
using WeatherApp.Services;
using WeatherProvider;

namespace WeatherApp.ViewModels
{
    internal class WeatherViewModel : Base.BaseViewModel
    {
        public RelayCommand RefreshDataCommand { get; }
        public RelayCommand SelectForecastCommand { get; }
        public RelayCommand BuildTempGraphCommand { get; }
        public RelayCommand BuildPressureGraphCommand { get; }
        public RelayCommand BuildWindGraphCommand { get; }
        public RelayCommand BuildHumidityGraphCommand { get; }



        public WeatherViewModel()
        {
            Title = "Главная";
            _refreshDataTimer.Elapsed += Timer_Elapsed;
            RefreshDataCommand = new RelayCommand(async (o) => await RefreshData());
            SelectForecastCommand = new RelayCommand((f) => SelectedForecast = f as DayForecastModel);

            BuildTempGraphCommand = new RelayCommand(a => BuildTempGraph());


            RefreshDataCommand.Execute(null);
        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            RefreshDataCommand.Execute(null);
        }

        private WeatherForecast _forecast;
        public WeatherForecast Forecast
        {
            get => _forecast;
            set => Set(ref _forecast, value, nameof(Forecast));
        }

        private DayForecastModel _selectedForecast;

        public DayForecastModel SelectedForecast
        {
            get => _selectedForecast;
            set => Set(ref _selectedForecast, value, nameof(SelectedForecast));
        }

        private readonly System.Timers.Timer _refreshDataTimer = new System.Timers.Timer()
        {
            AutoReset = true,
            Interval = 3600000,
        };

        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => Set(ref _isRefreshing, value, nameof(IsRefreshing));
        }

        private bool _errorReceivingData;

        public bool ErrorReceivingData
        {
            get => _errorReceivingData;
            set => Set(ref _errorReceivingData, value, nameof(ErrorReceivingData));
        }

        private async Task RefreshData()
        {
            IsRefreshing = true;
            if (ServiceManager.InternetConnectionService.IsConnectionExists)
            {
                Forecast = await WeatherService.GetWeatherAsync(54.6f, 39.7f)!;
                if (Forecast == null)
                {
                    ErrorReceivingData = true;
                    IsRefreshing = false;
                    return;
                }
                SelectedForecast = Forecast.DayForecasts.Where(p => p.Date == DateTime.Now.Date).FirstOrDefault()!;
                ErrorReceivingData = false;
            }
            else
            {
                await Task.Delay(700);
                ErrorReceivingData = true;
            }
            IsRefreshing = false;
        }

        private ISeries[] _series;
        public ISeries[] Series
        {
            get => _series;
            set => Set(ref _series, value, nameof(Series));
        }

        private void BuildTempGraph()
        {
            Series = new ISeries[1]
            {
                new LineSeries<double>
                {
                    Name = App.Current.Resources[Forecast.Measures.Temperature.ToString()].ToString(),
                    Values = SelectedForecast.HourlyForecasts.Select(p => Math.Round(p.Temperature, 3, MidpointRounding.AwayFromZero)),
                    Stroke = new LinearGradientPaint(
                        gradientStops : new[]{ new SKColor(45, 64, 89), new SKColor(255, 212, 96)}, 
                        startPoint : new SKPoint(0, 0), 
                        endPoint : new SKPoint(1, 0), 
                        tileMode : SKShaderTileMode.Clamp) 
                        { StrokeThickness = 5 },
                    GeometryStroke = new LinearGradientPaint(
                        gradientStops : new[]{ new SKColor(45, 64, 89), new SKColor(255, 212, 96)},
                        startPoint : new SKPoint(0, 0),
                        endPoint : new SKPoint(1, 0),
                        tileMode : SKShaderTileMode.Clamp)
                        { StrokeThickness = 5 },
                    Fill = null
                }
            };
        }

    }
}
