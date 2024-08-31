using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace mystery_app.Converters;
public class ByteOpacityConverter : IMultiValueConverter
{
    public static readonly ByteOpacityConverter Instance = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count == 2 && values[0] is byte byte1 && values[1] is byte byte2)
        {
            double opacityPercent = byte2 / 255.0;
            return (byte) (opacityPercent * byte1);
        }

        return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

}
