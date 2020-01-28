namespace WinPowerHelper.Wpf.Extensions
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows.Data;

    public class EnumDescriptionConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enu = value as Enum;
            var memberInfos = enu.GetType().GetMember(enu.ToString());
            if (memberInfos != null)
            {
                var description = memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false)[0] as DescriptionAttribute;
                return description.Description;
            }
            return enu.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
