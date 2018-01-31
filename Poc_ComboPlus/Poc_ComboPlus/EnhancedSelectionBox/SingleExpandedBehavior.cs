using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

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

        /// <summary>
        /// Helper class
        /// </summary>
        private class VisualTreeHelperEx
        {
            /// <summary>
            /// Recherche le parent du type spécifié
            /// </summary>
            /// <typeparam name="T">Le type à rechercher</typeparam>
            public static T FindAncestorByType<T>(DependencyObject targetDpObj) where T : DependencyObject
            {
                if (targetDpObj == null)
                {
                    return default(T);
                }

                if (targetDpObj is T)
                {
                    return (T)targetDpObj;
                }

                T parentObj = default(T);
                parentObj = FindAncestorByType<T>(VisualTreeHelperEx.GetParent(targetDpObj));
                return parentObj;
            }

            /// <summary>
            /// Retourne la liste des enfants dans l'arbre visuel du type specifié
            /// </summary>
            /// <typeparam name="childItem">Le type à rechercher</typeparam>
            public static IEnumerable<childItem> FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                    if (child != null && child is childItem)
                    {
                        yield return (childItem)child;
                    }
                    else
                    {
                        foreach (childItem childOfChild in FindVisualChild<childItem>(child))
                        {
                            yield return childOfChild;
                        }
                    }
                }

                yield break;
            }

            /// <summary>
            /// Find the specified parent node
            /// Find time considering FramworkElement and FramworkContentElement in two cases
            /// </summary>
            public static DependencyObject GetParent(DependencyObject targetDPObj)
            {
                Visual visualElement = targetDPObj as Visual;
                if (visualElement == null)
                {
                    return null;
                }

                DependencyObject parentObj = VisualTreeHelper.GetParent(visualElement);
                if (parentObj == null)
                {
                    FrameworkElement frameworkElement = targetDPObj as FrameworkElement;
                    if (frameworkElement != null)
                    {
                        parentObj = frameworkElement.Parent;
                        if (parentObj == null)
                        {
                            parentObj = frameworkElement.TemplatedParent;
                        }
                    }
                }
                else
                {
                    FrameworkContentElement fce = targetDPObj as FrameworkContentElement;
                    if (fce != null)
                    {
                        parentObj = fce.Parent;
                        if (parentObj == null)
                        {
                            parentObj = fce.TemplatedParent;
                        }
                    }
                }

                return parentObj;
            }
        }
    }
}