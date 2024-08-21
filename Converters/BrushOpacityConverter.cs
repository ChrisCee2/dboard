using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace mystery_app.Converters;
public class BrushOpacityConverter : IValueConverter
{
    public static readonly BrushOpacityConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is byte opacity && parameter is SolidColorBrush brush)
        {
            double opacityPercent = opacity / 255.0;
            Color color = brush.Color;
            return new SolidColorBrush(new Color((byte)(color.A * opacityPercent), color.R, color.G, color.B));
        }
        return null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }

}
