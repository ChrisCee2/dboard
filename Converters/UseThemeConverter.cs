using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Logging;
using Avalonia.Styling;

namespace mystery_app.Converters;
public class UseThemeConverter : IMultiValueConverter
{
    public static readonly UseThemeConverter Instance = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count == 3 && values[0] is ThemeVariant theme && values[1] is string expectedTheme && values[2] is bool useTheme && useTheme)
        {
            if (expectedTheme == "Dark")
            {
                Logger.TryGet(LogEventLevel.Fatal, LogArea.Control)?.Log(this, theme.Key.ToString());
                Logger.TryGet(LogEventLevel.Fatal, LogArea.Control)?.Log(this, ThemeVariant.Dark.ToString());
                Logger.TryGet(LogEventLevel.Fatal, LogArea.Control)?.Log(this, (theme == ThemeVariant.Dark).ToString());
                return theme == ThemeVariant.Dark;
            }
            else if (expectedTheme == "Light")
            {
                Logger.TryGet(LogEventLevel.Fatal, LogArea.Control)?.Log(this, theme.Key.ToString());
                Logger.TryGet(LogEventLevel.Fatal, LogArea.Control)?.Log(this, ThemeVariant.Light.ToString());
                Logger.TryGet(LogEventLevel.Fatal, LogArea.Control)?.Log(this, (theme == ThemeVariant.Light).ToString());
                return theme == ThemeVariant.Light;
            }
        }
        return false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }

}
