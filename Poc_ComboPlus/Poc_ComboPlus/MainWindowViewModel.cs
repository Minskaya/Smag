using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace Poc_ComboPlus
{
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
            this.LoadItems();
        }

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
                foreach (var id in Enumerable.Range(1, 9999))
                {
                    items.Add(new Item(id));
                }

                return items;
            })
            .ContinueWith(items =>
            {
                InvokeOnUIThread(() => this.Items.Reset(items.Result));
            });
        }
    }

    public class Item
    {
        public Item()
        {
        }

        public Item(int i)
        {
            this.Regroupement = string.Format(
                "Regroupement {0}",
                (i / 100) + 1);

            this.Name = string.Format(
                "Element {0}",
                i);

            this.Id = i;
        }

        public string Regroupement { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }
}