using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Poc_ComboPlus
{
    /// <summary>
    /// Helper class
    /// </summary>
    public class VisualTreeHelperEx
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
        /// Retourne le parent selon si targetDPObj est un FramworkElement ou un FramworkContentElement
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