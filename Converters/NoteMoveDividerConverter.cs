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
            values[0] is bool isTopDivider &&
            values[1] is bool cursorIsAboveNote &&
            values[2] is NoteViewModel noteToMove &&
            values[3] is NoteViewModel noteCursorWasLastOn)
        {
            return isTopDivider == cursorIsAboveNote && noteToMove == noteCursorWasLastOn;
        }
        return false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return false;
    }

}
