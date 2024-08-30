using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

namespace mystery_app.Converters;
public class MultiplicationConverter : IMultiValueConverter
{
    public static readonly MultiplicationConverter Instance = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        double product = 1;
        foreach (var val in values)
        {
            if (val is double num) { product *= num; }
            else { return product; }
        }
        return product;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }

}