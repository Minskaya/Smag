using System;
using System.Globalization;
using System.Windows.Data;

namespace DecimalMarkupExtension
{
    public class DecimalPrecisionConverter : IMultiValueConverter
    {
        private NumberScalingFormatter formatter = new NumberScalingFormatter(/*CultureInfo.CurrentCulture*/);

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double number = System.Convert.ToDouble(values[0]);
            int precision = values.Length > 1
                            ? System.Convert.ToInt32(values[1])
                            : CultureInfo.CurrentCulture.NumberFormat.NumberDecimalDigits;

            ScalingFactor scale;
            Enum.TryParse<ScalingFactor>(parameter as string, out scale);

            return string.Format(
                formatter,
                "{0:N" + precision + ":" + scale + "}",
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