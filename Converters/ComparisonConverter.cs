﻿using System;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace mystery_app.Converters;
public class ComparisonConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value?.Equals(parameter);
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value?.Equals(true) == true ? parameter : BindingOperations.DoNothing;
    }
}