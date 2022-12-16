using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.ViewModels
{
    internal class WeatherViewModel : Base.BaseViewModel
    {
        public WeatherViewModel()
        {
            Title = "WeatherPage";
        }

        public List<int> WeatherDays => new List<int>() { 1, 2, 3, 4, 5 ,6, 7,8 ,9 ,10};
    }
}
