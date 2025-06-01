using System;
using System.Globalization;
using Avalonia.Data.Converters;
using dboard.ViewModels;

namespace dboard.Converters;
public class StringToTextEditConverter : IValueConverter
{
    public static readonly StringToTextEditConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is TextEditViewModel viewModel)
        {
            return viewModel.Text;
        }

        return "";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string text)
        {
            return viewModel.Text;
        }

        return "";
    }

}
