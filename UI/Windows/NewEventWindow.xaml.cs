using System.Windows;
using UI.Dto;

namespace UI.Windows;
public partial class NewEventWindow : Window
{
    public NewEventWindow()
    {
        InitializeComponent();
    }


    private async void CreateEvent_Click(object sender, RoutedEventArgs e)
    {
        var client = new ApiClient();
        await client.CreateEventsAsync(new CreateCalendarEventRequest() { Start = StartDatePicker.SelectedDate.Value, End = EndDatePicker.SelectedDate.Value, Attributes = null });
    }
}