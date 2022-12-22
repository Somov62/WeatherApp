using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WeatherApp.Views
{
    /// <summary>
    /// Логика взаимодействия для WeatherPage.xaml
    /// </summary>
    public partial class WeatherPage : Page
    {
        public WeatherPage()
        {
            InitializeComponent();
        }


        private void ListView_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Shift)
                return;

            var offset = outsideScrollView.VerticalOffset - e.Delta;

            outsideScrollView.ScrollToVerticalOffset(offset);
        }

        private void outsideScrollView_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (graphicsPanel.IsMouseOver)
                {
                    var chartcore = chart.CoreChart as CartesianChart<SkiaSharpDrawingContext>;
                    chartcore.Zoom(new LiveChartsCore.Drawing.LvcPoint(0, 0), LiveChartsCore.Measure.ZoomDirection.ZoomIn);
                    e.Handled = true;
                }                
            }
        }
    }
}
