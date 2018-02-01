using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace Poc_ComboPlus
{
    public class ListBox : RadListBox
    {
        public ListBox()
            : base()
        {
            this.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;
        }

        private void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
        {
            this.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;
            var item = VisualTreeHelperEx.FindVisualChild<Expander>(this)
                        .FirstOrDefault();
            if (item != null)
            {
                item.IsExpanded = true;
            }
        }
    }
}