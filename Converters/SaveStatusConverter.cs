using System;
using System.Globalization;
using Avalonia.Data.Converters;
using System.Collections.Generic;
using dboard.Constants;

namespace dboard.Converters;
public class SaveStatusConverter : IMultiValueConverter
{
    public static readonly SaveStatusConverter Instance = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values[1] is WorkspaceConstants.SAVE_STATUS save_status)
        {
            string workspace_name = values[0] is null | values[0] is not string ? "New Workspace" : (string)values[0];
            return workspace_name + " - " + WorkspaceConstants.save_status_text[save_status];
        }
        return "";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }

}
