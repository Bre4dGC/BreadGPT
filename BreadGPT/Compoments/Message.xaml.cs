using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BreadGPT.Compoments
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

        /// <summary>
        /// Message Content
        /// </summary>
        public string MessageContent
        {
            get => (string)GetValue(MessageContentProperty);
            set => SetValue(MessageContentProperty, value);
        }

        public static readonly DependencyProperty MessageContentProperty =
            DependencyProperty.Register(nameof(MessageContent), typeof(string), typeof(Message));

        /// <summary>
        /// Message Background Color
        /// </summary>
        public new SolidColorBrush Background
        {
            get => (SolidColorBrush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public static readonly new DependencyProperty BackgroundProperty =
            DependencyProperty.Register(nameof(Background), typeof(SolidColorBrush), typeof(Message), new PropertyMetadata(new BrushConverter().ConvertFrom("#282E34")));
    }
}
