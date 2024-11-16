using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BreadGPT.Styles.Compoments
{
    /// <summary>
    /// Логика взаимодействия для MessageInput.xaml
    /// </summary>
    public partial class MessageInput : UserControl
    {
        public MessageInput()
        {
            InitializeComponent();
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(MessageInput), new PropertyMetadata(string.Empty));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(MessageInput), new PropertyMetadata(null));
    }
}
