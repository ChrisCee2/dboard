using System;
using System.Globalization;
using Avalonia.Data.Converters;
using System.Collections.Generic;
using Avalonia.Data;
using Avalonia;

namespace mystery_app.Converters;
public class RectanglePositionConverter : IMultiValueConverter
{
    public static readonly RectanglePositionConverter Instance = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values[0] is Point pressPosition && values[1] is Point cursorPosition && parameter is string axis)
        {
            if (axis == "X")
            {
                return Math.Min(pressPosition.X, cursorPosition.X);
            }
            else if (axis == "Y")
            {
                return Math.Min(pressPosition.Y, cursorPosition.Y);
            }
        }

        return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

}