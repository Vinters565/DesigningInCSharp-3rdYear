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
    public bool IsPublic { get; set; } = false;

    public DateTime StartDate;

    public NewEventWindow(bool isEdit, DateTime startDate, bool isPublic)
    {
        InitializeComponent();
        Instance?.Close();
        Instance = this;
        StartDate = startDate;
        IsPublic = isPublic;
        if (isEdit)
        {
            OpenEditEventPage();
        }
        else 
        {
            OpenEventInfoPage();
        }
    }

    private void OpenEditEventPage()
    {
        EventFrame.NavigationService.Navigate(new EditEventPage(StartDate));
    }

    private void OpenEventInfoPage()
    {
        EventFrame.NavigationService.Navigate(new EventInfoPage());
    }
}