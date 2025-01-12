using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace UI.Resources;
public class ThemeManager
{
    private const string ThemeKey = "SecondaryColor";

    public static void ApplyPrimaryColor(Color newColor)
    {
        var primaryColorResource = Application.Current.Resources.MergedDictionaries
            .SelectMany(dict => dict.Keys.Cast<object>())
            .FirstOrDefault(key => key.ToString() == ThemeKey);

        if (primaryColorResource != null)
        {
            Application.Current.Resources[ThemeKey] = newColor;
            Application.Current.Resources["SecondaryBrush"] = new SolidColorBrush(newColor);
        }
    }
}
