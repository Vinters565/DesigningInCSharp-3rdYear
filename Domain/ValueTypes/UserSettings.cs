using System.Drawing;

namespace SchedulePlanner.Domain.ValueTypes;

public class UserSettings(string displayedName, Color primaryColor, Color secondaryColor)
{
    public string DisplayedName { get; init; } = displayedName;
    public Color PrimaryColor { get; init; } = primaryColor;
    public Color SecondaryColor { get; init; } = secondaryColor;
}
