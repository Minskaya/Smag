using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace darshitdaveCombo
{
    public class HeightConverter : IMultiValueConverter
    {
        /// <param name="values">
        ///There will be two values here - first one will be the collection group bound - one call per group in combo box.
        /// The second will be the desired items to be displayed when the group is not expanded.
        /// /// <param name="targetType">
        /// <param name="parameter">
        /// <param name="culture">
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double returnValue;
            // Find out the total number of items in the current group i.e. 4 items in Most Recently Used group.
            int itemCount = ((System.Windows.Data.CollectionViewGroup)(((System.Windows.FrameworkElement)(values[0])).DataContext)).ItemCount;
            // This is the number of items to be displayed when the group is not expanded.
            int configuredItems = (int)values[1];
            // Now, if there are less items available in particular group than configured, then we will display all the items available in group
            // else we will display the configured number of items only. This means, if configured item number is 5 but there are only 3 items in
            // any group, then we will consider 3 items and will return height to display exact 3 items only.
            if (configuredItems >= itemCount)
            {
                // SystemParameters.SmallIconHeight - return the required height for one row - this is required because
                // if you configured it hard coded, then when user will change the resolution, the number of items displayed will not be
                // what you wish i.e. if resolution is incresed, then instead of 3 - it will display 4 items and vice versa.
                returnValue = itemCount * SystemParameters.SmallIconHeight;
            }
            else
            {
                returnValue = configuredItems * SystemParameters.SmallIconHeight;
            }

            // This 2 is added just to ensure some space is left at the end of combo box.
            return returnValue + 2;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}