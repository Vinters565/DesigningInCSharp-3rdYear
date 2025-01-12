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
    private EditEventPage editEventPage;
    public bool Public { get; set; } = false;

    public DateTime? StartDate
    {
        get => editEventPage.StartDate;
        set => editEventPage.StartDate = value;
    }

    public NewEventWindow()
    {
        InitializeComponent();
        Instance?.Close();
        Instance = this;
        editEventPage = new EditEventPage();
        OpenEditEventPage();
    }

    private void OpenEditEventPage()
    {
        EventFrame.NavigationService.Navigate(new EditEventPage());
    }

    private void OpenEventInfoPage()
    {
        EventFrame.NavigationService.Navigate(new EventInfoPage());
    }
}