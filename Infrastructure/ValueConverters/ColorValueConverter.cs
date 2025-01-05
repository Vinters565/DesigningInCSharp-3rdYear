using System.Drawing;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchedulePlanner.Utils.Extensions;

namespace SchedulePlanner.Infrastructure.ValueConverters;

public class ColorValueConverter : ValueConverter<Color, string>
{
    public ColorValueConverter() 
        : base(
            color => color.ToHex(),
            value => ColorExtensions.FromHex(value))
    {
    }
}