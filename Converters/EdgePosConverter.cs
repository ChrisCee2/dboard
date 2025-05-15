using System;
using System.Globalization;
using Avalonia.Data.Converters;
using System.Collections.Generic;
using dboard.Tools;

namespace dboard.Converters;
public class EdgePosConverter : IMultiValueConverter
{
    public static readonly EdgePosConverter Instance = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count == 3 && values[0] is double x && values[1] is double y && values[2] is double width)
        {
            return EdgeTools.EdgePosFromNode(x, y, width);
        }
        return null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

}
