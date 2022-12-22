using System.Windows;
using System.Windows.Controls.Primitives;

namespace WeatherApp.Resources.Controls
{
    /// <summary>
    /// Логика взаимодействия для CustomToggleButton.xaml
    /// </summary>
    public partial class CustomToggleButton : ToggleButton        
    {
        public CustomToggleButton()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty RightCaseProperty = DependencyProperty.Register("RightCase", typeof(string), typeof(CustomToggleButton));
        public static readonly DependencyProperty LeftCaseProperty = DependencyProperty.Register("LeftCase", typeof(string), typeof(CustomToggleButton));

        public string RightCase
        {
            get { return (string)GetValue(RightCaseProperty); }
            set { SetValue(RightCaseProperty, value); }
        } 
        
        public string LeftCase
        {
            get { return (string)GetValue(LeftCaseProperty); }
            set { SetValue(LeftCaseProperty, value); }
        }
    }
}
