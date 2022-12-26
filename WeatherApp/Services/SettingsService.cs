using GeoCoder.Models;
using Models;
using Newtonsoft.Json;
using System;
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
            config ??= new Configuration()
            {
                Culture = Cultures.EN,
                Lenght = LenghtMeasure.Mm,
                Temperature = TemperatureMeasure.Celsius,
                Pressure = PressureMeasure.MmHg,
                Wind = WindSpeed.Ms,
                SelectedLocation = new GeoLocation() { Latitude = 54.629566f, Longitude = 39.741917f, Name = "Рязань", Description = "Россия" }
            };
            if (!config.FavouritLocations.Contains(config.SelectedLocation)) config.FavouritLocations.Add(config.SelectedLocation);
            ServiceManager.LocalizationService.SetCulture(config.Culture);
            return config;
        }
    }

    public class Configuration
    {

        public GeoLocation SelectedLocation { get; set; }

        public ObservableCollection<GeoLocation> FavouritLocations { get; set; } = new ObservableCollection<GeoLocation>();

        public Cultures Culture { get; set; }
        public bool IsSavingTrafficModeEnabled { get; set; }

        public TemperatureMeasure Temperature { get; set; }
        public PressureMeasure Pressure { get; set; }
        public LenghtMeasure Lenght { get; set; }
        public WindSpeed Wind { get; set; }

    }


}
