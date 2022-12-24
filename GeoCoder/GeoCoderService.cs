using GeoCoder.Cache;
using GeoCoder.Models;
using System.Globalization;
using System.Net.Http.Json;
using System.Text;

namespace GeoCoder
{
    public static class GeoCoderService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly string _apikey = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Apikey.ini")).ReplaceLineEndings();

        public async static Task<List<GeoLocation>> GetPosition(string place)
        {
            var cachedLocations = LocationsRepository.Find(place);
            if (cachedLocations.Count > 0) return cachedLocations;


            StringBuilder url = new();
            url.Append("https://geocode-maps.yandex.ru/1.x");
            url.Append("?format=json");
            url.Append("&results=20");
            url.Append("&apikey=" + _apikey);
            url.Append("&geocode=" + place);

            ApiResponse response;
            try
            {
                response = await _httpClient.GetFromJsonAsync<ApiResponse>(new Uri(url.ToString()));
            }
            catch (Exception ex)
            {
                return null!;
            }

            var model = ToUserModel(response);
            LocationsRepository.SaveArrange(model);
            return model;
        }

        private static List<GeoLocation> ToUserModel(ApiResponse response)
        {
            List<GeoLocation> locations = new List<GeoLocation>();


            foreach (var item in response.Response.GeoObjectCollection.featureMember)
            {
                GeoLocation location = new GeoLocation();

                location.Name = item.GeoObject.name;
                location.Description = item.GeoObject.description;
                var points = item.GeoObject.Point.pos.Split(' ').ToArray();
                location.Latitude = float.Parse(points[1], CultureInfo.InvariantCulture.NumberFormat);
                location.Longitude = float.Parse(points[0], CultureInfo.InvariantCulture.NumberFormat);
                locations.Add(location);
            }

            return locations;
        }
    }
}