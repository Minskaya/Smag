using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Media;

namespace Poc_ComboPlus
{
    public class IdToColorConverter : IValueConverter
    {
        private static PropertyInfo[] AvailableColors = typeof(Colors).GetProperties();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var idColor = (int)value % AvailableColors.Length;
            var colorName = AvailableColors[idColor].Name;

            return colorName == "White"
                   ? "#FFAB6666"
                   : colorName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}