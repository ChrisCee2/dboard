using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace mystery_app.Converters;
public class PercentStringConverter : IValueConverter
{
    public static readonly PercentStringConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double d)
        {
            return ((int) (d * 100)).ToString() + '%';
        }
        return null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }

}
