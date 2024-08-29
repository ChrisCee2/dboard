using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace mystery_app.Converters;
public class GridLengthDivisionConverter : IValueConverter
{
    public static readonly GridLengthDivisionConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double num1 && double.Parse((string)parameter) is double num2)
        {
            return new GridLength(num1 / num2);
        }
        return new GridLength(0);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }

}
