using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
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
        public RelayCommand BuildHumidityGraphCommand { get; }



        public WeatherViewModel()
        {
            Title = App.Current.Resources["WeatherPageTitle"].ToString();
            _refreshDataTimer.Elapsed += Timer_Elapsed;
            RefreshDataCommand = new RelayCommand(async (o) => await RefreshData());
            SelectForecastCommand = new RelayCommand((f) => SelectedForecast = f as DayForecastModel);
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
            set
            {
                Set(ref _selectedForecast, value, nameof(SelectedForecast));
                if (!IsWeekTimeSelected) IsTempChecked = true;
            }
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
            await Task.Delay(300);
            if (ServiceManager.InternetConnectionService.IsConnectionExists)
            {
                Forecast = await WeatherService.GetWeatherAsync(SettingsService.Configuration.SelectedLocation.Latitude, SettingsService.Configuration.SelectedLocation.Longitude, SettingsService.Configuration.SelectedLocation.Name)!;
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

        public override void OnAppearing()
        {
            if (Forecast is null)
            {
                RefreshDataCommand.Execute(null);
                return;
            }
            if (Forecast.Location.Latitude != SettingsService.Configuration.SelectedLocation.Latitude ||
                Forecast.Location.Longitude != SettingsService.Configuration.SelectedLocation.Longitude)
            {
                RefreshDataCommand.Execute(null);
                return;
            }

            if (Forecast.Measures.PrecipitationSum != SettingsService.Configuration.Lenght ||
                Forecast.Measures.Temperature != SettingsService.Configuration.Temperature ||
                Forecast.Measures.Windspeed != SettingsService.Configuration.Wind ||
                Forecast.Measures.Pressure != SettingsService.Configuration.Pressure)
                RefreshDataCommand.Execute(null);

            base.OnAppearing();
        }

        #region Graphics

        private bool _isTempChecked;
        public bool IsTempChecked
        {
            get => _isTempChecked;
            set
            {
                Set(ref _isTempChecked, value, nameof(IsTempChecked));
                if (value) BuildTempGraph();
            }
        }

        private bool _isPressureChecked;
        public bool IsPressureChecked
        {
            get => _isPressureChecked;
            set
            {
                Set(ref _isPressureChecked, value, nameof(IsPressureChecked));
                if (value) BuildPressureGraph();
            }
        }

        private bool _isRelativeHumidityChecked;
        public bool IsRelativeHumidityChecked
        {
            get => _isRelativeHumidityChecked;
            set
            {
                Set(ref _isRelativeHumidityChecked, value, nameof(IsRelativeHumidityChecked));
                if (value) BuildHumidityGraph();
            }
        }

        private bool _isWeekTimeSelected;

        public bool IsWeekTimeSelected
        {
            get => _isWeekTimeSelected;
            set
            {
                Set(ref _isWeekTimeSelected, value, nameof(IsWeekTimeSelected));
                IsTempChecked = true;
                if (Forecast is null) return;
                if (value)
                {
                    AxisX = new List<Axis>() { new Axis { Labels = Forecast.DayForecasts.Select(p => p.Date.ToString("dd.MM")).ToList() } };
                }
                else
                {
                    AxisX = new List<Axis>() { new Axis { Labels = SelectedForecast.HourlyForecasts.Select(p => p.Time.ToString("t")).ToList() } };
                }
            }
        }


        private ISeries[] _series;
        public ISeries[] Series
        {
            get => _series;
            set => Set(ref _series, value, nameof(Series));
        }

        private List<Axis> _axisX;
        public List<Axis> AxisX
        {
            get => _axisX;
            set => Set(ref _axisX, value, nameof(AxisX));
        }


        private void BuildTempGraph()
        {
            if (Forecast is null) return;

            var series = new LineSeries<double>
            {
                Name = App.Current.Resources[Forecast.Measures.Temperature.ToString()].ToString(),
                Stroke = new LinearGradientPaint(
                        gradientStops: new[] { new SKColor(245, 175, 25), new SKColor(241, 39, 17) },
                        startPoint: new SKPoint(0, 0), endPoint: new SKPoint(1, 0),
                        tileMode: SKShaderTileMode.Clamp)
                { StrokeThickness = 5 },
                GeometryStroke = new LinearGradientPaint(
                        gradientStops: new[] { new SKColor(245, 175, 25), new SKColor(241, 39, 17) },
                        startPoint: new SKPoint(0, 0), endPoint: new SKPoint(1, 0),
                        tileMode: SKShaderTileMode.Clamp)
                { StrokeThickness = 5 },
                Fill = null
            };
            Series = new ISeries[1] { series };

            if (IsWeekTimeSelected)
            {
                series.Values = Forecast.DayForecasts.Select(p => Math.Round(p.MaxTemperature, 3, MidpointRounding.AwayFromZero));

                Series = new ISeries[2] { series,  new LineSeries<double>
                {
                    Name = App.Current.Resources[Forecast.Measures.Temperature.ToString()].ToString(),
                    Values = Forecast.DayForecasts.Select(p => Math.Round(p.MinTemperature, 3, MidpointRounding.AwayFromZero)),
                    Stroke =  new LinearGradientPaint(
                                gradientStops: new[] { new SKColor(9, 198, 249), new SKColor(4, 93, 233) },
                                startPoint: new SKPoint(0, 0), endPoint: new SKPoint(1, 0),
                                tileMode: SKShaderTileMode.Clamp) { StrokeThickness = 5 },
                    GeometryStroke = new LinearGradientPaint(
                                gradientStops: new[] { new SKColor(9, 198, 249), new SKColor(4, 93, 233) },
                                startPoint: new SKPoint(0, 0), endPoint: new SKPoint(1, 0),
                                tileMode: SKShaderTileMode.Clamp) { StrokeThickness = 5 },
                    Fill = null
                }};
                return;
            }
            series.Values = SelectedForecast.HourlyForecasts.Select(p => Math.Round(p.Temperature, 3, MidpointRounding.AwayFromZero));

        }

        private void BuildPressureGraph()
        {
            if (Forecast is null) return;

            var series = new LineSeries<double>
            {
                Name = App.Current.Resources[Forecast.Measures.Pressure.ToString()].ToString(),
                Stroke = new LinearGradientPaint(
                        gradientStops: new[] { new SKColor(220, 227, 91), new SKColor(69, 182, 73) },
                        startPoint: new SKPoint(0, 0), endPoint: new SKPoint(1, 0),
                        tileMode: SKShaderTileMode.Clamp)
                { StrokeThickness = 5 },
                GeometryStroke = new LinearGradientPaint(
                        gradientStops: new[] { new SKColor(220, 227, 91), new SKColor(69, 182, 73) },
                        startPoint: new SKPoint(0, 0), endPoint: new SKPoint(1, 0),
                        tileMode: SKShaderTileMode.Clamp)
                { StrokeThickness = 5 },
                Fill = null
            };

            Series = new ISeries[1] { series };
            if (IsWeekTimeSelected)
            {
                series.Values = Forecast.DayForecasts.Select(p => Math.Round(p.HourlyForecasts.Average(p => p.SurfasePressure), 3, MidpointRounding.AwayFromZero));
                return;
            }
            series.Values = SelectedForecast.HourlyForecasts.Select(p => Math.Round(p.SurfasePressure, 3, MidpointRounding.AwayFromZero));
        }

        private void BuildHumidityGraph()
        {
            if (Forecast is null) return;

            var series = new LineSeries<double>
            {
                Name = App.Current.Resources["RelativeHumidity"].ToString() + " %",
                Stroke = new LinearGradientPaint(
                       gradientStops: new[] { new SKColor(9, 198, 249), new SKColor(4, 93, 233) },
                       startPoint: new SKPoint(0, 0), endPoint: new SKPoint(1, 0),
                       tileMode: SKShaderTileMode.Clamp)
                { StrokeThickness = 5 },
                GeometryStroke = new LinearGradientPaint(
                       gradientStops: new[] { new SKColor(9, 198, 249), new SKColor(4, 93, 233) },
                       startPoint: new SKPoint(0, 0), endPoint: new SKPoint(1, 0),
                       tileMode: SKShaderTileMode.Clamp)
                { StrokeThickness = 5 },
                Fill = null
            };

            Series = new ISeries[1] { series };

            if (IsWeekTimeSelected)
            {
                series.Values = Forecast.DayForecasts.Select(p => Math.Round(p.HourlyForecasts.Average(p => p.RelativeHumidity), 3, MidpointRounding.AwayFromZero));
                return;
            }
            series.Values = SelectedForecast.HourlyForecasts.Select(p => Math.Round(p.RelativeHumidity, 3, MidpointRounding.AwayFromZero));
        }

        #endregion
    }
}
