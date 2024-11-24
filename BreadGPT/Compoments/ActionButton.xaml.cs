using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BreadGPT.Compoments
{
    /// <summary>
    /// Логика взаимодействия для ActionButton.xaml
    /// </summary>
    public partial class ActionButton : UserControl
    {
        public ActionButton()
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
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(ActionButton));



        /// <summary>
        /// Команды для кнопки
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(ButtonCommandProperty);
            set => SetValue(ButtonCommandProperty, value);
        }

        public static readonly DependencyProperty ButtonCommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(ActionButton), new PropertyMetadata(null));



        /// <summary>
        /// Иконка для кнопки
        /// </summary>
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register(nameof(Icon), typeof(ImageSource), typeof(ActionButton));
    }
}
