using System.Windows;
using System.Windows.Controls;
using UI.Dto;
using UI.UserControls;

namespace UI.Windows;
public partial class NewEventWindow : Window
{
    private static NewEventWindow Instance = null!;

    public bool Public { get; set; } = false;

    public DateTime? StartDate
    {
        get => StartDatePicker.DateTime;
        set => StartDatePicker.DateTime = value;
    }

    public NewEventWindow()
    {
        InitializeComponent();
        Instance?.Close();
        Instance = this;
    }

    private void CreateEvent_Click(object sender, RoutedEventArgs e)
    {

    }
}