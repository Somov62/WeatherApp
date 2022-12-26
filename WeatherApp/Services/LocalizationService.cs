using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;

namespace WeatherApp.Services
{
    internal class LocalizationService
    {
        private Dictionary<Cultures, ResourceDictionary> _dictionary = new Dictionary<Cultures, ResourceDictionary>()
        {
            { Cultures.EN, new ResourceDictionary() { Source = new Uri("pack://application:,,,/Resources/Localization/lang.en-US.xaml", UriKind.RelativeOrAbsolute) } },
            { Cultures.RU, new ResourceDictionary() { Source = new Uri("pack://application:,,,/Resources/Localization/lang.ru-RU.xaml", UriKind.RelativeOrAbsolute) } }
        };
        public Cultures? CurrentCulture { get; }

        public void SetCulture(Cultures culture)
        {
            if (CurrentCulture == culture) return;
            if (CurrentCulture is not null)
                App.Current.Resources.MergedDictionaries.Remove(_dictionary[CurrentCulture.Value]);
            App.Current.Resources.MergedDictionaries.Add(_dictionary[culture]);
            CultureInfo.CurrentCulture = new CultureInfo(App.Current.Resources["lang"].ToString());
        }
    }
    public enum Cultures
    {
        EN,
        RU
    }
}
