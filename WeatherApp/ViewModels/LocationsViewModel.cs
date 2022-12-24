using GeoCoder;
using GeoCoder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.ViewModels
{
    class LocationsViewModel : Base.BaseViewModel
    {
        public LocationsViewModel()
        {
            Title = App.Current.Resources["LocationsPageTitle"].ToString();
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

        

    }
}
