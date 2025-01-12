using CommunityToolkit.Mvvm.Messaging;
using Syncfusion.Windows.Shared;
using System.Windows;
using System.Windows.Controls;
using UI.Messages;
using UI.Resources;
using System.Windows.Media;

namespace UI.ElementPage
{
    /// <summary>
    /// Логика взаимодействия для PersonalAccountPage.xaml
    /// </summary>
    public partial class PersonalAccountPage : Page
    {
        private string _selectedColor;

        public static readonly Dictionary<string, Color> ColorsVariant = new()
    {
        { "Красный", Colors.Red },
        { "Синий", Colors.Blue },
        { "Зелёный", Colors.Green },
        { "Оранжевый", Colors.Orange },
        { "Фиолетовый", Colors.Purple }
    };

        public PersonalAccountPage()
        {
            InitializeComponent();
        }

        private void ExitAccount_Click(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Send(new ExitAccountMessage());
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Send(new BackMessage());
        }

        private void ColorPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ColorPicker.SelectedItem is string selectedColor)
            {
                _selectedColor = selectedColor;
            }
        }

        private void ApplyColor_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_selectedColor) && ColorsVariant.TryGetValue(_selectedColor, out var color))
            {
                ThemeManager.ApplyPrimaryColor(color);
            }
        }
    }
}
