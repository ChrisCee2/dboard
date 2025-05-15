using System;
using System.Globalization;
using Avalonia.Data.Converters;
using System.Collections.Generic;
using Avalonia.Data;
using System.Linq;

namespace dboard.Converters;
public class MedianConverter : IMultiValueConverter
{
    public static readonly MedianConverter Instance = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        double median = 0;
        foreach (var value in values)
        {
            if (value is double number)
            {
                median += number;
            }
            else { return new BindingNotification(new InvalidCastException(), BindingErrorType.Error); }
        }
        return median / values.Count();
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

}
