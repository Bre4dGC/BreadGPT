using System.Collections.Specialized;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace BreadGPT.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Подписка на событие 
            ((INotifyCollectionChanged)MessageItemsControl.Items).CollectionChanged += Items_CollectionChanged;
        }

        bool _isSidebarOpen;
        /// <summary>
        /// Сворачивание и разворачивание сайдбара с анимацией
        /// </summary>
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

        /// <summary>
        /// Выход из приложения по кнопке
        /// </summary>
        private void TabButton_ButtonClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Сворачивание окна по кнопки
        /// </summary>
        private void TabButton_ButtonClicked_1(object sender, RoutedEventArgs e)
        {
            Window.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Перемещение и сворачивание окна
        /// </summary>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
                WindowState = WindowState.Minimized;

            if (e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }

        /// <summary>
        /// Отправка сообщения по нажатию Enter
        /// </summary>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Input.Command.Execute(null);
            }
        }

        /// <summary>
        /// Чтобы скролл всегда был на последних сообщениях
        /// </summary>
        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            MessageScrollViewer.ScrollToBottom();
        }
    }
}