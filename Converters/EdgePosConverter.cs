using System;
using System.Globalization;
using Avalonia.Data.Converters;
using System.Collections.Generic;
using mystery_app.Tools;
using Avalonia;

namespace mystery_app.Converters;
public class EdgePosConverter : IMultiValueConverter
{
    public static readonly EdgePosConverter Instance = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count == 3 && values[0] is double x && values[1] is double y && values[2] is double pinWidth && values[2] is double width)
        {
            Point pos = EdgeTools.EdgePosFromNode(x, y, pinWidth, width);
            if (parameter is string axis)
            {
                if (axis == "X")
                {
                    return pos.X;
                }
                else if (axis == "Y")
                {
                    return pos.Y;
                }

            }

            return pos;
        }
        return null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

}
