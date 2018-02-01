using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Poc_ComboPlus
{
    /// <summary>
    /// Behavior permettant de n'avoir qu'un seul expander déplié dans la radlistbox
    /// </summary>
    public class SingleExpandedBehavior : Behavior<Expander>
    {
        /// <summary>
        /// S'attache à l'evenement Expanded
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.Expanded += new RoutedEventHandler(ExpanderHasExpanded);
        }

        /// <summary>
        /// Libère l'event handler de l'evenement Expanded
        /// </summary>

        protected override void OnDetaching()
        {
            this.AssociatedObject.Expanded -= new RoutedEventHandler(ExpanderHasExpanded);
            base.OnDetaching();
        }

        /// <summary>
        /// Gestionnaire de l'evenement Expanded
        /// Parcours tous les 'freres' de type Expander pour replier celui qui est ouvert
        /// </summary>
        private void ExpanderHasExpanded(object sender, RoutedEventArgs e)
        {
            //var listbox = VisualTreeHelperEx.FindAncestorByType<ListBox>(sender as DependencyObject);
            var listbox = VisualTreeHelperEx.FindAncestorByType<Telerik.Windows.Controls.RadListBox>(sender as DependencyObject);

            foreach (var item in VisualTreeHelperEx.FindVisualChild<Expander>(listbox))
            {
                if (item != sender && item.IsExpanded)
                {
                    item.IsExpanded = false;
                    break;
                }
            }
        }
    }
}