using System;
using System.Globalization;
using Avalonia.Controls.Primitives;
using Avalonia.Data.Converters;
using Avalonia.Logging;

namespace dboard.Converters;
public class ReverseScaleConverter : IValueConverter
{
    public static readonly ReverseScaleConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double scale)
        {
            Logger.TryGet(LogEventLevel.Fatal, LogArea.Control)?.Log(this, (Math.Round(1.0 / scale, 3)).ToString());
            return Math.Round(1.0 / scale, 3);
        }

        return 1.0;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return 1.0;
    }

}
