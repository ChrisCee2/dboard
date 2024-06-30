using System;
using System.Globalization;
using Avalonia.Data.Converters;
using System.Collections.Generic;
using mystery_app.ViewModels;
using Avalonia.Data;
using Avalonia;

namespace mystery_app.Converters;
public class EdgeMidPointConverter : IMultiValueConverter
{
    public static readonly EdgeMidPointConverter Instance = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values[0] is Point fromPosition && values[1] is Point toPosition && parameter is string axis)
        {
            var midPoint = (fromPosition + toPosition) / 2;
            if (axis == "X")
            {
                return midPoint.X;
            }
            else if (axis == "Y")
            {
                return midPoint.Y;
            }
        }
        // converter used for the wrong type
        return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

}
