using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BreadGPT.Styles.Compoments
{
    /// <summary>
    /// Логика взаимодействия для Message.xaml
    /// </summary>
    public partial class Message : UserControl
    {
        public Message()
        {
            InitializeComponent();
        }

        public string MessageContent
        {
            get => (string)GetValue(MessageContentProperty);
            set => SetValue(MessageContentProperty, value);
        }

        public static readonly DependencyProperty MessageContentProperty =
            DependencyProperty.Register(nameof(MessageContent), typeof(string), typeof(Message), new PropertyMetadata(string.Empty));

        // Цвет фона сообщения
        public Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(nameof(Background), typeof(Brush), typeof(Message), new PropertyMetadata(Brushes.LightGray));
    }
}
