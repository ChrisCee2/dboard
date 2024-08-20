using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media.Immutable;

namespace mystery_app.Converters;
public class AddConverter : IMultiValueConverter
{
    public static readonly AddConverter Instance = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        double sum = 0;
        foreach (var val in values)
        {
            if (val is double num) { sum += num; }
            else { return sum; }
        }
        return sum;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }

}
