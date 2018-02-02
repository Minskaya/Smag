using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace Poc_ComboPlus
{
    /// <summary>
    /// Custom RadListBox pour force l'expand du premier expander
    /// </summary>
    public class ListBox : RadListBox
    {
        /// <summary>
        /// Constructeur statique
        /// </summary>
        static ListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListBox), new FrameworkPropertyMetadata(typeof(ListBox)));
        }

        /// <summary>
        /// Appellé quand la source de donnée change
        /// </summary>
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            this.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;

            base.OnItemsChanged(e);
        }

        /// <summary>
        /// Handler de l'evenement StatusChanged
        /// </summary>
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