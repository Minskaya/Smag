using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace Poc_ComboPlus
{
    public class Item
    {
        public Item(int i, int idRegroupement)
        {
            this.Name = string.Format("Phyto {0}", i);
            this.RegroupementId = idRegroupement;
            this.Regroupement = string.Format(
                "Catégorie {0}",
                this.RegroupementId);

            this.Id = i;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Regroupement { get; set; }
        public int RegroupementId { get; set; }
    }

    public class MainWindowViewModel : ViewModelBase
    {
        private Item selectedItem;

        /// <summary>
        /// Title />
        /// </summary>
        private string title = "Poc combo améliorée";

        public MainWindowViewModel()
        {
            this.Items = new SmartCollection<Item>();
            this.GroupedItems = new CollectionViewSource();
            this.GroupedItems.Source = this.Items;
            this.GroupedItems.GroupDescriptions.Add(new PropertyGroupDescription("Regroupement"));
            this.GroupedItems.SortDescriptions.Add(new SortDescription("RegroupementId", ListSortDirection.Ascending));
            this.GroupedItems.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Descending));
            this.LoadItems();
        }

        public CollectionViewSource GroupedItems { get; private set; }
        public SmartCollection<Item> Items { get; private set; }

        public Item SelectedItem
        {
            get
            {
                return this.selectedItem;
            }
            set

            {
                this.selectedItem = value;
                this.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets The property's value
        /// </summary>
        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
                this.RaisePropertyChanged();
            }
        }

        private void LoadItems()
        {
            Task.Run(() =>
            {
                List<Item> items = new List<Item>();
                Random rnd = new Random();
                foreach (var id in Enumerable.Range(1, 9999))
                {
                    items.Add(new Item(id, rnd.Next(1, 99)));
                }

                return items;
            })
            .ContinueWith(items =>
            {
                InvokeOnUIThread(() =>
                {
                    this.Items.Reset(items.Result);
                });
            });
        }
    }
}