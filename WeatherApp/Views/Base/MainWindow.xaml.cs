using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace WeatherApp.Views.Base
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized) this.WindowState = WindowState.Normal;
            this.DragMove();
        }


        #region ResizeWindows
        bool ResizeInProcess = false;
        private void Resize_Init(object sender, MouseButtonEventArgs e)
        {
            Rectangle senderRect = sender as Rectangle;
            if (senderRect != null)
            {
                ResizeInProcess = true;
                senderRect.CaptureMouse();
            }
        }

        private void Resize_End(object sender, MouseButtonEventArgs e)
        {
            Rectangle senderRect = sender as Rectangle;
            if (senderRect != null)
            {
                ResizeInProcess = false; ;
                senderRect.ReleaseMouseCapture();
            }
        }

        private void Resizeing_Form(object sender, MouseEventArgs e)
        {
            if (!ResizeInProcess) return;

            double temp = 0;
            Rectangle senderRect = sender as Rectangle;
            Window mainWindow = senderRect.Tag as Window;

            if (senderRect == null) return;

            double width = e.GetPosition(mainWindow).X;
            double height = e.GetPosition(mainWindow).Y;
            senderRect.CaptureMouse();
            if (senderRect.Name.Contains("right", StringComparison.OrdinalIgnoreCase))
            {
                width += 5;
                if (width > 0)
                    mainWindow.Width = width;
            }
            if (senderRect.Name.Contains("left", StringComparison.OrdinalIgnoreCase))
            {
                width -= 5;
                temp = mainWindow.Width - width;
                if ((temp > mainWindow.MinWidth) && (temp < mainWindow.MaxWidth))
                {
                    mainWindow.Width = temp;
                    mainWindow.Left += width;
                }
            }
            if (senderRect.Name.Contains("bottom", StringComparison.OrdinalIgnoreCase))
            {
                height += 5;
                if (height > 0)
                    mainWindow.Height = height;
            }
            if (senderRect.Name.ToLower().Contains("top", StringComparison.OrdinalIgnoreCase))
            {
                height -= 5;
                temp = mainWindow.Height - height;
                if ((temp > mainWindow.MinHeight) && (temp < mainWindow.MaxHeight))
                {
                    mainWindow.Height = temp;
                    mainWindow.Top += height;
                }
            }


        }

        #endregion

        private void MaximazedButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized) this.WindowState = WindowState.Normal;
            else this.WindowState = WindowState.Maximized;
        }

        private void MinimazedButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }
    }
}
