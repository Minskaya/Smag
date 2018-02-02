using System;
using System.Globalization;
using System.Windows.Data;

namespace Poc_ComboPlus
{
    /// <summary>
    /// Converter entre <see cref="Poc_ComboPlus.SelectionMode"/> et <see cref="Telerik.Windows.Controls.Primitives.AutoCompleteSelectionMode"/>
    /// </summary>
    [ValueConversion(typeof(Poc_ComboPlus.SelectionMode), typeof(Telerik.Windows.Controls.Primitives.AutoCompleteSelectionMode))]
    public class SelectionModeConverter : IValueConverter
    {
        /// <summary>
        /// Conversion de <see cref="Poc_ComboPlus.SelectionMode"/> vers <see cref="Telerik.Windows.Controls.Primitives.AutoCompleteSelectionMode"/>
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (Telerik.Windows.Controls.Primitives.AutoCompleteSelectionMode)(int)value;
        }

        /// <summary>
        /// Conversion de <see cref="Telerik.Windows.Controls.Primitives.AutoCompleteSelectionMode"/> vers <see cref="Poc_ComboPlus.SelectionMode"/>
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (Poc_ComboPlus.SelectionMode)(int)value;
        }
    }
}