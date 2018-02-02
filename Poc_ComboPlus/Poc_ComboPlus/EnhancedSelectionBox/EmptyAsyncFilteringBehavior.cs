using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Primitives;

namespace Poc_ComboPlus
{
    /// <summary>
    /// Filtre asynchrone permettant de retourner les résultats pour une saisie vide
    /// </summary>
    public sealed class EmptyAsyncFilteringBehavior : IFilteringBehavior, IAsyncItemSearch, IValueRetriever, IDisposable
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        public EmptyAsyncFilteringBehavior()
        {
            filter.ItemsFound += Filter_ItemsFound;
        }

        /// <summary>
        /// Evenement lancé quand la recherche a abouti
        /// </summary>
        public event EventHandler<AsyncItemSearchEventArgs> ItemsFound;

        /// <summary>
        /// Dispose l'instance
        /// </summary>
        public void Dispose()
        {
            filter.Dispose();
        }

        /// <summary>
        /// Lance une recherche asynchone
        /// </summary>
        public void FindItems(Predicate<object> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            filter.FindItems(match);
        }

        /// <summary>
        /// Lance une recherche
        /// </summary>
        public IEnumerable<object> FindMatchingItems(
            string searchText,
            System.Collections.IList items,
            IEnumerable<object> escapedItems,
            string textSearchPath,
            TextSearchMode textSearchMode)
        {
            if (items == null)
            {
                return Enumerable.Empty<object>();
            }

            if (escapedItems == null)
            {
                throw new ArgumentNullException(nameof(escapedItems));
            }

            if (string.IsNullOrWhiteSpace(searchText))
            {
                Task.Factory.StartNew(
                    () =>
                    {
                        if (this.ItemsFound != null)
                        {
                            var itemsFound = items.OfType<object>().Where(x => !escapedItems.Contains(x));
                            if (this.ItemsFound != null)
                            {
                                this.ItemsFound(null, new AsyncItemSearchEventArgs(itemsFound));
                            }
                        }
                    },
                CancellationToken.None,
                TaskCreationOptions.AttachedToParent,
                TaskScheduler.FromCurrentSynchronizationContext());

                return Enumerable.Empty<object>();
            }
            else
            {
                return filter.FindMatchingItems(
                    searchText,
                    items,
                    escapedItems,
                    textSearchPath,
                    textSearchMode);
            }
        }

        /// <summary>
        /// Represents value Retrievers which are used in the autocomplete behaviors.
        /// </summary>
        public object GetValue(object item)
        {
            return filter.GetValue(item);
        }

        /// <summary>
        /// Filtre asynchrone pour les recherches non vides
        /// </summary>
        private AsyncFilteringBehavior filter = new AsyncFilteringBehavior();

        /// <summary>
        /// Handler de l'evenement ItemsFound de l'AsyncFilteringBehavior
        /// </summary>
        private void Filter_ItemsFound(object sender, AsyncItemSearchEventArgs e)
        {
            if (this.ItemsFound != null)
            {
                this.ItemsFound(this, e);
            }
        }
    }
}