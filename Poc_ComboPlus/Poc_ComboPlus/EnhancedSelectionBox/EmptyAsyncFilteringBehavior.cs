using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Primitives;

namespace Poc_ComboPlus
{
    public class EmptyAsyncFilteringBehavior : IFilteringBehavior, IAsyncItemSearch, IValueRetriever, IDisposable
    {
        public EmptyAsyncFilteringBehavior()
        {
            filter.ItemsFound += Filter_ItemsFound;
        }

        public event EventHandler<AsyncItemSearchEventArgs> ItemsFound;

        public void Dispose()
        {
            filter.Dispose();
        }

        public void FindItems(Predicate<object> match)
        {
            filter.FindItems(match);
        }

        public IEnumerable<object> FindMatchingItems(
            string searchText,
            System.Collections.IList items,
            IEnumerable<object> escapedItems,
            string textSearchPath,
            TextSearchMode textSearchMode)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                Task.Factory.StartNew(
                    () =>
                    {
                        if (this.ItemsFound != null)
                        {
                            var itemsFound = items.OfType<object>().Where(x => !escapedItems.Contains(x));
                            this.ItemsFound(null, new AsyncItemSearchEventArgs(itemsFound));
                        }
                    },
                CancellationToken.None,
                TaskCreationOptions.AttachedToParent,
                TaskScheduler.FromCurrentSynchronizationContext());

                //return items.OfType<object>().Where(x => !escapedItems.Contains(x));

                return Enumerable.Empty<object>();
            }
            else
            {
                //return base.FindMatchingItems(searchText, items, escapedItems, textSearchPath, textSearchMode);

                return filter.FindMatchingItems(
                    searchText,
                    items,
                    escapedItems,
                    textSearchPath,
                    textSearchMode);
            }
        }

        public object GetValue(object item)
        {
            return filter.GetValue(item);
        }

        private AsyncFilteringBehavior filter = new AsyncFilteringBehavior();

        private void Filter_ItemsFound(object sender, AsyncItemSearchEventArgs e)
        {
            if (this.ItemsFound != null)
            {
                this.ItemsFound(this, e);
            }
        }
    }
}