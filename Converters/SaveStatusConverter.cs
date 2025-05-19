using System;
using System.Globalization;
using Avalonia.Data.Converters;
using System.Collections.Generic;
using Avalonia.Media;
using dboard.Constants;

namespace dboard.Converters;
public class SaveStatusConverter : IMultiValueConverter
{
    public static readonly SaveStatusConverter Instance = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count == 2 && values[0] is string workspace_name && values[1] is WorkspaceConstants.SAVE_STATUS save_status)
        {
            return new Color(a, r, g, b);
        }
        return null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }

}
