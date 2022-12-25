using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Drawing;
using System.Windows.Controls;
using System.Windows.Input;

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

        private void ScrollToLeft_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            selectForecastScroll.LineLeft();
            selectForecastScroll.LineLeft();
            selectForecastScroll.LineLeft();
            selectForecastScroll.LineLeft();
            selectForecastScroll.LineLeft();
            selectForecastScroll.LineLeft();
            selectForecastScroll.LineLeft();
            selectForecastScroll.LineLeft();
            selectForecastScroll.LineLeft();
            selectForecastScroll.LineLeft();
            selectForecastScroll.LineLeft();
        }

        private void ScrollToRight_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            selectForecastScroll.LineRight();
            selectForecastScroll.LineRight();
            selectForecastScroll.LineRight();
            selectForecastScroll.LineRight();
            selectForecastScroll.LineRight();
            selectForecastScroll.LineRight();
            selectForecastScroll.LineRight();
            selectForecastScroll.LineRight();
            selectForecastScroll.LineRight();
            selectForecastScroll.LineRight();
            selectForecastScroll.LineRight();
        }
    }
}
