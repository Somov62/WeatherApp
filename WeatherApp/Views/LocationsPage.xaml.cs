using System.Windows.Controls;
using System.Windows.Input;

namespace WeatherApp.Views
{
    /// <summary>
    /// Логика взаимодействия для LocationsPage.xaml
    /// </summary>
    public partial class LocationsPage : Page
    {
        public LocationsPage()
        {
            InitializeComponent();
        }

        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }
    }
}
