using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BreadGPT.Compoments
{
    public partial class TabButton : UserControl
    {
        public TabButton()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Команды для кнопки
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(ButtonCommandProperty);
            set => SetValue(ButtonCommandProperty, value);
        }

        public static readonly DependencyProperty ButtonCommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(TabButton), new PropertyMetadata(null));

        /// <summary>
        /// Иконка для кнопки
        /// </summary>
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register(nameof(Icon), typeof(ImageSource), typeof(TabButton));

        /// <summary>
        /// Размер иконки
        /// </summary>
        public int IconSize
        {
            get { return (int)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        public static readonly new DependencyProperty SizeProperty =
            DependencyProperty.Register(nameof(IconSize), typeof(int), typeof(TabButton));

        /// <summary>
        /// Событие клика
        /// </summary>
        public event RoutedEventHandler ButtonClicked;

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            // Вызов события, если есть подписчики
            ButtonClicked?.Invoke(this, e);
        }
    }
}
