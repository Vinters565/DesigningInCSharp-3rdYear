using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using UI.Messages;
using UI.Windows;
using UI.Dto;

namespace UI.ElementPage
{
    public partial class EventInfoPage : Page
    {
        private CalendarEventDto eventDto;
        public EventInfoPage(CalendarEventDto calendarEvent)
        {
            InitializeComponent();
            eventDto = calendarEvent;
            EventNameTextBox.Text = "Новое событие";
            StartDatePicker.DateTime = calendarEvent.Start;
            EndDatePicker.DateTime = calendarEvent.End;
        }

        private void EditEventButton_Click(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Send(new CloseEventWindowMessage());
        }

        private async void DeleteEventButton_Click(object sender, RoutedEventArgs e)
        {
            await App.ServiceProvider.GetRequiredService<ApiClient>().DeleteEventAsync(eventDto.Id);
            WeakReferenceMessenger.Default.Send(new UpdateViewMessage());
            WeakReferenceMessenger.Default.Send(new CloseEventWindowMessage());
        }
    }
}
