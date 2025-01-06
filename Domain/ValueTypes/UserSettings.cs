using System.Drawing;
using Microsoft.EntityFrameworkCore;

namespace SchedulePlanner.Domain.ValueTypes;

[Owned]
public class UserSettings
{
    public static readonly Color DefaultPrimaryColor = Color.Cyan;
    public static readonly Color DefaultSecondaryColor = Color.DarkCyan;

    public string DisplayedName { get; set; } = null!;
    
    public Color PrimaryColor { get; set; }
    public Color SecondaryColor { get; set; }
    
    public UserSettings() { } // EF Core
    
    public UserSettings(string displayedName, Color? primaryColor = null, Color? secondaryColor = null)
    {
        DisplayedName = displayedName;
        PrimaryColor = primaryColor ?? DefaultPrimaryColor;
        SecondaryColor = secondaryColor ?? DefaultSecondaryColor;
    }
}
