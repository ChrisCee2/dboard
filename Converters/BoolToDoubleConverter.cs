using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace mystery_app.Converters;
public class BoolToDoubleConverter : IValueConverter
{
    public static readonly BoolToDoubleConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool val)
        {
            return (bool)value ? double.Parse((string)parameter) : 0;
        }
        return null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }

}
