using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Windows.Controls;

namespace UI.Messages;

public class ViewCalendarMessage : ValueChangedMessage<UserControl>
{
    public ViewCalendarMessage(UserControl value) : base(value) { }
}
