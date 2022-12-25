using WeatherApp.Mvvm;
using WeatherApp.Services;

namespace WeatherApp.ViewModels
{
    internal class MainViewModel : Base.BaseViewModel
    {
        private WeatherViewModel _weatherPage;
        private LocationsViewModel _locationsPage;
        private SettingsViewModel _settingsPage;

        public MainViewModel()
        {
            NavigationService.ViewChanged += ViewChanged;
            ToWeatherPageCommand = new ((v) => NavigationService.SetView(_weatherPage ??= new ()));
            ToLocationsPageCommand = new ((v) => NavigationService.SetView(_locationsPage ??= new ()));
            ToSettingsPageCommand = new ((v) => NavigationService.SetView(_settingsPage ??= new ()));
            ToWeatherPageCommand.Execute(null);
        }

        private void ViewChanged(Base.BaseViewModel actualView)
        {
            OnPropertyChanged(nameof(CurrentView));
        }

        public RelayCommand ToWeatherPageCommand { get; }
        public RelayCommand ToLocationsPageCommand { get; }
        public RelayCommand ToSettingsPageCommand { get; }


        public Base.BaseViewModel CurrentView => NavigationService.CurrentView;
    }
}
