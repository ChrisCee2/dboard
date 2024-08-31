using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

namespace dboard.Converters;
public class ThicknessMultConverter : IMultiValueConverter
{
    public static readonly ThicknessMultConverter Instance = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values[0] is double padding && values[1] is double factor)
        {
            return new Avalonia.Thickness(padding * factor);
        }
        return new Avalonia.Thickness(0);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }

}
