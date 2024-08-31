using System;
using System.Globalization;
using Avalonia.Data.Converters;
using System.Collections.Generic;
using Avalonia.Media;

namespace dboard.Converters;

public class StrCheckBgConverter : IMultiValueConverter
{
    public static readonly StrCheckBgConverter Instance = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count == 4 && values[0] is string str1 && values[1] is string str2 && values[2] is SolidColorBrush bg1 && values[3] is SolidColorBrush bg2)
        {
            if (str1.Equals(str2))
            {
                return bg1;
            }
            else
            {
                return bg2;
            }
        }
        return null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }

}