using System.Globalization;
using System.Windows.Data;

namespace WeatherApp.Mvvm
{
    public class CultureAwareBinding : Binding
    {
        public CultureAwareBinding()
        {
            ConverterCulture = CultureInfo.CurrentCulture;
        }
    }
}
