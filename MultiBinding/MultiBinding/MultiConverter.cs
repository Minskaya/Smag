using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MultiBinding
{
    public class MultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue)
            {
                return string.Empty;
            }

            double number = System.Convert.ToDouble(values[0], CultureInfo.CurrentCulture);

            return string.Format("{0:N2}", number);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            double number = System.Convert.ToDouble(value, CultureInfo.CurrentCulture);

            return new object[]
            {
                number,
                Binding.DoNothing
            };
        }
    }
}