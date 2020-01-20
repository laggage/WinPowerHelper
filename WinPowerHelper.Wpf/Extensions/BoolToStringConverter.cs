namespace WinPowerHelper.Wpf.Extensions
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class BoolToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(bool)) return new InvalidCastException();

            var splitText = (parameter as string).Split(';');
            if (splitText == null || splitText.Length != 2)
                return value; // 转换失败
            return ((bool) value) ? splitText[0] : splitText[1];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
