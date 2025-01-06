using System.Drawing;

namespace SchedulePlanner.Utils.Extensions;

public static class ColorExtensions
{
    public static string ToHex(this Color color)
    {
        return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
    }
    
    public static Color FromHex(string hex)
    {
        if (string.IsNullOrEmpty(hex) || hex[0] != '#')
        {
            throw new ArgumentException("Invalid HEX format.", nameof(hex));
        }

        return Color.FromArgb(
            Convert.ToInt32(hex.Substring(1, 2), 16),
            Convert.ToInt32(hex.Substring(3, 2), 16),
            Convert.ToInt32(hex.Substring(5, 2), 16)
        );
    }
}