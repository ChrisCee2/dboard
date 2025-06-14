using System;
using System.Globalization;
using Avalonia.Data.Converters;
using System.Collections.Generic;
using dboard.ViewModels;
using Avalonia.Media;

namespace dboard.Converters;
public class NoteMoveHighlightConverter : IMultiValueConverter
{
    public static readonly NoteMoveHighlightConverter Instance = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        BoxShadow boxShadow = new BoxShadow();
        if (
            values[0] is NoteViewModel currentNote &&
            values[1] is NoteViewModel noteToMove &&
            values[2] is Color highlightColor &&
            currentNote == noteToMove)
        {
            boxShadow.Color = highlightColor;
            boxShadow.Spread = 2;
        }

        return new BoxShadows(boxShadow);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return false;
    }

}
