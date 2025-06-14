using System;
using System.Globalization;
using Avalonia.Data.Converters;
using System.Collections.Generic;
using dboard.ViewModels;

namespace dboard.Converters;
public class NoteMoveDividerConverter : IMultiValueConverter
{
    public static readonly NoteMoveDividerConverter Instance = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (
            values[0] is bool cursorIsInRightDivider &&
            values[1] is NoteViewModel currentNote &&
            values[2] is NoteViewModel noteCursorWasLastOn)
        {
            return cursorIsInRightDivider && currentNote == noteCursorWasLastOn;
        }

        return false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return false;
    }

}
