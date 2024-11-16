using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BreadGPT.Styles.Compoments
{
    public partial class ChatNavButton : UserControl
    {
        public ChatNavButton()
        {
            InitializeComponent();
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(ChatNavButton));

        public ICommand Command
        {
            get => (ICommand)GetValue(ButtonCommandProperty);
            set => SetValue(ButtonCommandProperty, value);
        }

        public static readonly DependencyProperty ButtonCommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(ChatNavButton), new PropertyMetadata(null));

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            grid.Visibility = Visibility.Visible;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            grid.Visibility = Visibility.Collapsed;
        }
    }
}
