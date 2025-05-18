using System;
using System.Globalization;
using Avalonia.Data.Converters;
using AvaloniaEdit.Document;
namespace dboard.Converters;
public class DocumentStringConverter : IValueConverter
{
    public static readonly DocumentStringConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is String text) return new TextDocument(value as string);
        return new TextDocument("");
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is TextDocument document) return document.Text;
        return "";
    }

}