using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace mystery_app.Converters;
public class MaxHeightConverter : IValueConverter
{
    public static readonly MaxHeightConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double val && double.Parse((string)parameter) is double percentage)
        {
            return val * percentage;
        }
        return null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }

}
