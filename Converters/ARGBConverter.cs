using System;
using System.Globalization;
using Avalonia.Data.Converters;
using System.Collections.Generic;
using Avalonia.Media;
using Avalonia.Media.Immutable;

namespace mystery_app.Converters;
public class ARGBConverter : IMultiValueConverter
{
    public static readonly ARGBConverter Instance = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count == 4 && values[0] is byte a && values[1] is byte r && values[2] is byte g && values[3] is byte b)
        {
            return new ImmutableSolidColorBrush(new Color(a, r, g, b));
        }
        return null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

}
