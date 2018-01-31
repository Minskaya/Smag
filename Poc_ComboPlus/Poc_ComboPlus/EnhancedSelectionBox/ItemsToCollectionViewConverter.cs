using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace Poc_ComboPlus
{
    [ValueConversion(typeof(IEnumerable), typeof(ICollectionView))]
    public class ItemsToCollectionViewConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.Length == 2)
            {
                var propertyName = values[1] as string;

                var groupedCountries = new CollectionViewSource();
                groupedCountries.GroupDescriptions.Add(new PropertyGroupDescription(propertyName));
                groupedCountries.Source = values[0] as IEnumerable;

                return groupedCountries.View;
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}