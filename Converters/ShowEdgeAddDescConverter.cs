using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

namespace dboard.Converters;
public class ShowEdgeAddDescConverter : IMultiValueConverter
{
    public static readonly ShowEdgeAddDescConverter Instance = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count == 2 && values[0] is null && values[1] is bool isSelected && isSelected)
        {
            return true;
        }
        return false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }

}
