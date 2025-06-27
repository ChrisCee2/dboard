using System;
using System.Globalization;
using Avalonia.Data.Converters;
using System.Collections.Generic;

namespace dboard.Converters;
public class EdgeDescPositionConverter : IMultiValueConverter
{
    public static readonly EdgeDescPositionConverter Instance = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count == 3 && values[0] is double edge1 && values[1] is double edge2 && values[2] is double size)
        {
            return ((edge1 + edge2) / 2) - (size / 2); 
        }
        return 0;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

}
