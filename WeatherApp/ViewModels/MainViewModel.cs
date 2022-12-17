using WeatherApp.Mvvm;
using WeatherApp.Services;

namespace WeatherApp.ViewModels
{
    internal class MainViewModel : Base.BaseViewModel
    {
        private WeatherViewModel _weatherPage;
        private LocationsViewModel _locationsPage;



        public MainViewModel()
        {
            NavigationService.ViewChanged += ViewChanged;
            ToWeatherPageCommand = new ((v) => NavigationService.SetView(_weatherPage ??= new ()));
            ToLocationsPageCommand = new ((v) => NavigationService.SetView(_locationsPage ??= new ()));
        }

        private void ViewChanged(Base.BaseViewModel actualView)
        {
            OnPropertyChanged(nameof(CurrentView));
        }

        public RelayCommand ToWeatherPageCommand { get; }
        public RelayCommand ToLocationsPageCommand { get; }


        public Base.BaseViewModel CurrentView => NavigationService.CurrentView;



    }
}
