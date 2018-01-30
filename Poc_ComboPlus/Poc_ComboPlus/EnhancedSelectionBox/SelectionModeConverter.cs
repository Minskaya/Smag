using System;
using System.Globalization;
using System.Windows.Data;

namespace Poc_ComboPlus
{
    [ValueConversion(typeof(Poc_ComboPlus.SelectionMode), typeof(Telerik.Windows.Controls.Primitives.AutoCompleteSelectionMode))]
    public class SelectionModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Telerik.Windows.Controls.Primitives.AutoCompleteSelectionMode)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Poc_ComboPlus.SelectionMode)value;
        }
    }
}