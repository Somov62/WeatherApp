using GeoCoder;
using GeoCoder.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WeatherApp.Mvvm;
using WeatherApp.Services;

namespace WeatherApp.ViewModels
{
    class LocationsViewModel : Base.BaseViewModel
    {
        public RelayCommand SelectLocationCommand { get; }

        public LocationsViewModel()
        {
            Title = App.Current.Resources["LocationsPageTitle"].ToString();
            SelectLocationCommand = new RelayCommand((location) => SelectedLocation = location as GeoLocation);
        }

        private string _locationSearch;

        public string LocationSearch
        {
            get => _locationSearch;
            set
            {
                Set(ref _locationSearch, value, nameof(LocationSearch));
                if (string.IsNullOrEmpty(value))
                {
                    SearchResults = null!;
                    return;
                }
                Task.Run(async () => { SearchResults = await GeoCoderService.GetPosition(value); });
            }
        }

        private List<GeoLocation> _searchResults;

        public List<GeoLocation> SearchResults
        {
            get => _searchResults;
            set => Set(ref _searchResults, value, nameof(SearchResults));
        }

        public ObservableCollection<GeoLocation> FavouritLocations => SettingsService.Configuration.FavouritLocations;

        public GeoLocation SelectedLocation
        {
            get => SettingsService.Configuration.SelectedLocation;
            set
            {
                SearchResults = null!;
                if (value is not null)
                {
                    SettingsService.Configuration.SelectedLocation = value;
                    if (!FavouritLocations.Contains(value)) FavouritLocations.Add(value);
                }

                OnPropertyChanged(nameof(SelectedLocation));
            }
        }


    }
}
