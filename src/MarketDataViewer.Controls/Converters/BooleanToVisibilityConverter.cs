﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MarketDataViewer.Controls.Converters
{
    /// <summary>
    /// Converts boolean value to Visibility and v.v.
    ///   True - Visible
    ///   False - Collapsed
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool) && ((bool)value)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is Visibility) && ((Visibility)value) == Visibility.Visible
                ? true
                : false;
        }
    }
}
