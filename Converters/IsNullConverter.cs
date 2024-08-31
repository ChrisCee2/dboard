using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace dboard.Converters;
public class IsNullConverter : IValueConverter
{
    public static readonly IsNullConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }

}
