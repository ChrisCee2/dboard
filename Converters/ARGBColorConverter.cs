using System;
using System.Globalization;
using Avalonia.Data.Converters;
using System.Collections.Generic;
using Avalonia.Media;
using Avalonia.Logging;

namespace mystery_app.Converters;
public class ARGBColorConverter : IMultiValueConverter
{
    public static readonly ARGBBrushConverter Instance = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count == 4 && values[0] is byte a && values[1] is byte r && values[2] is byte g && values[3] is byte b)
        {
            return new Color(a, r, g, b);
        }
        return null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        Logger.TryGet(LogEventLevel.Fatal, LogArea.Control)?.Log(this, targetType.FullName);
        if (value is Color color) 
        {
            return new List<byte> { color.A, color.R, color.G, color.B };
        }
        return null;
    }

}
