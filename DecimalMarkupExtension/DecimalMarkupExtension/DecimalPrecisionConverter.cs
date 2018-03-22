using System;
using System.Globalization;
using System.Windows.Data;

namespace DecimalMarkupExtension
{
    public class DecimalPrecisionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double number = System.Convert.ToDouble(values[0]);
            int precision = System.Convert.ToInt32(values[1]);

            ScalingFactor scale;
            Enum.TryParse<ScalingFactor>(parameter as string, out scale);

            NumberScalingFormatter formatter = new NumberScalingFormatter(scale, CultureInfo.CurrentCulture);
            return string.Format(
                formatter,
                "{0:N" + precision + "}",
                number);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[]
            {
                Binding.DoNothing
            };
        }
    }
}