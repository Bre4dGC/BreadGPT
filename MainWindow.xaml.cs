using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace BreadGPT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        bool _isSidebarOpen;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_isSidebarOpen)
            {
                Content.BeginAnimation(WidthProperty,
                    new DoubleAnimation
                    {
                        From = Content.Width,
                        To = Content.Width += Sidebar.Width,
                        Duration = new Duration(TimeSpan.FromSeconds(0.3)),
                        EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
                    });

                _isSidebarOpen = false;
            }

            else
            {
                Content.BeginAnimation(WidthProperty,
                    new DoubleAnimation
                    {
                        From = Content.Width,
                        To = Content.Width -= Sidebar.Width,
                        Duration = new Duration(TimeSpan.FromSeconds(0.3)),
                        EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
                    });

                _isSidebarOpen = true;
            }
        }

        private void TabButton_ButtonClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TabButton_ButtonClicked_1(object sender, RoutedEventArgs e)
        {
            Window.WindowState = WindowState.Minimized;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
                WindowState = WindowState.Minimized;

            if (e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }
    }
}