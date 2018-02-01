using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace Poc_ComboPlus
{
    public class ListBox : RadListBox
    {
        static ListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListBox), new FrameworkPropertyMetadata(typeof(ListBox)));
        }

        public ListBox()
        {
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            this.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;

            base.OnItemsChanged(e);
        }

        private void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
        {
            var item = VisualTreeHelperEx.FindVisualChild<Expander>(this)
                                         .FirstOrDefault();
            if (item != null)
            {
                this.ItemContainerGenerator.StatusChanged -= ItemContainerGenerator_StatusChanged;
                item.IsExpanded = true;
            }
        }
    }
}