using System;
using System.Globalization;
using Avalonia.Data.Converters;
using System.Collections.Generic;
using Avalonia.Data;

namespace mystery_app.Converters;
public class MinConverter : IMultiValueConverter
{
    public static readonly MinConverter Instance = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        double min = double.PositiveInfinity;
        foreach (var value in values)
        {
            if (value is double number)
            {
                if (number < min) { min = number; }
            }
            else { return new BindingNotification(new InvalidCastException(), BindingErrorType.Error); }
        }
        return min;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

}