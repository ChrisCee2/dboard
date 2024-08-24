using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace mystery_app.Converters;
public class CanvasSizeConverter : IValueConverter
{
    public static readonly CanvasSizeConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var num = 0;
        if (value is string val)
        {
            int.TryParse(val, out num);
        }
        if (parameter is string param && int.Parse(param) is int max)
        {
            return Math.Min(num, max);
        }
        return num;
    }

}
