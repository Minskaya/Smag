using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Poc_ComboPlus
{
    public class ItemStyleSelector : StyleSelector
    {
        public Style GroupStyle { get; set; }
        public Style ItemStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            return (item is CollectionViewGroup) ? this.GroupStyle : this.ItemStyle;
        }
    }
}