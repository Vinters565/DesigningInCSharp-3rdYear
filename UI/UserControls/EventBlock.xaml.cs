using System.Windows;
using System.Windows.Controls;

namespace UI.UserControls
{
    /// <summary>
    /// Логика взаимодействия для EventBlock.xaml
    /// </summary>
    public partial class EventBlock : UserControl
    {
        public EventBlock()
        {
            InitializeComponent();
        }

        public string EventTitle
        {
            get => (string)GetValue(EventTitleProperty);
            set => SetValue(EventTitleProperty, value);
        }

        public static readonly DependencyProperty EventTitleProperty =
            DependencyProperty.Register(nameof(EventTitle), typeof(string), typeof(EventBlock), new PropertyMetadata(string.Empty));

        public int StartRow
        {
            get => (int)GetValue(StartRowProperty);
            set => SetValue(StartRowProperty, value);
        }

        public static readonly DependencyProperty StartRowProperty =
            DependencyProperty.Register(nameof(StartRow), typeof(int), typeof(EventBlock), new PropertyMetadata(0));

        public int Duration
        {
            get => (int)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register(nameof(Duration), typeof(int), typeof(EventBlock), new PropertyMetadata(1));
    }
}
