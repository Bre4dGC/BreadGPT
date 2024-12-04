using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BreadGPT.Compoments
{
    public partial class ChatNavButton : UserControl
    {
        public ChatNavButton()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Chat Name
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(ChatNavButton));

        /// <summary>
        /// Button Command
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(ButtonCommandProperty);
            set => SetValue(ButtonCommandProperty, value);
        }

        public static readonly DependencyProperty ButtonCommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(ChatNavButton), new PropertyMetadata(null));

        /// <summary>
        /// Button IsChecked
        /// </summary>
        public bool IsChecked
        {
            get { return (bool)GetValue(CheckedProperty); }
            set { SetValue(CheckedProperty, value); }
        }

        public static readonly DependencyProperty CheckedProperty =
            DependencyProperty.Register(nameof(IsChecked), typeof(bool), typeof(ChatNavButton));

        #region Action menu Visibility
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            grid.Visibility = Visibility.Visible;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            grid.Visibility = Visibility.Collapsed;
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyMenuPopup.IsOpen = !MyMenuPopup.IsOpen;
        }
    }
}
