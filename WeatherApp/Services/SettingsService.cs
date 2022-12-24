using GeoCoder.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace WeatherApp.Services
{
    internal static class SettingsService 
    {
        private static readonly string _pathToFile = Path.Combine(Environment.CurrentDirectory, "appsettings.json");

        public static Configuration Configuration { get; } = Load();

        public static void SaveChanges()
        {
            File.WriteAllText(_pathToFile, JsonConvert.SerializeObject(Configuration));
        }

        private static Configuration Load()
        {
            if (!File.Exists(_pathToFile))
                File.Create(_pathToFile).Close();
            Configuration config = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(_pathToFile))!;
            return config ??= new Configuration();
        }
    }

    public class Configuration
    {

        public GeoLocation  SelectedLocation { get; set; }

        public ObservableCollection<GeoLocation> FavouritLocations { get; set; } = new ObservableCollection<GeoLocation>();


        public bool  IsSavingTrafficModeEnabled { get; set; }

    }


}
