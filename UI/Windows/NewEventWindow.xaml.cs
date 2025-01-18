using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.Messaging;
using UI.Dto;
using UI.ElementPage;
using UI.Messages;
using UI.UserControls;

namespace UI.Windows;
public partial class NewEventWindow : Window
{
    private static NewEventWindow Instance = null!;
    private CalendarEventDto eventDto;
    public bool IsPublic { get; set; } = false;

    public DateTime StartDate;

    public NewEventWindow(DateTime startDate, bool isPublic)
    {
        InitializeComponent();
        Instance?.Close();
        Instance = this;
        StartDate = startDate;
        IsPublic = isPublic;
        OpenEditEventPage();
        WeakReferenceMessenger.Default.Register<CloseEventWindowMessage>(this, (r, m) => Close());
    }

    public NewEventWindow(CalendarEventDto calendarEvent)
    {
        InitializeComponent();
        Instance?.Close();
        Instance = this;
        eventDto = calendarEvent;
        OpenEventInfoPage();
        WeakReferenceMessenger.Default.Register<CloseEventWindowMessage>(this, (r, m) => Close());
    }

    private void OpenEditEventPage()
    {
        EventFrame.NavigationService.Navigate(new EditEventPage(StartDate));
    }

    private void OpenEventInfoPage()
    {
        EventFrame.NavigationService.Navigate(new EventInfoPage(eventDto));
    }
}