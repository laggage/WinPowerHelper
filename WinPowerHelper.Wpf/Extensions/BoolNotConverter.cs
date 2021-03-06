﻿namespace WinPowerHelper.Wpf.Extensions
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class BoolNotConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return !((bool)value);
            }
            catch
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return !((bool)value);
            }
            catch
            {
                return value;
            }
        }
    }
}
