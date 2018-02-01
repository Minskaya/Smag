using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace Poc_ComboPlus
{
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
            this.RegroupementId = (i / 100) + 1;

            this.Name = string.Format(
                "Element {0}",
                i);

            this.Id = i;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Regroupement { get; set; }
        public int RegroupementId { get; set; }
    }

    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            this.Items = new SmartCollection<Item>();
            this.GroupedItems = new CollectionViewSource();
            this.GroupedItems.Source = this.Items;
            this.GroupedItems.GroupDescriptions.Add(new PropertyGroupDescription("Regroupement"));
            //this.EnhancedComboContext = new Poc_ComboPlus.EnhancedComboContext();

            this.LoadItems();
        }

        public EnhancedComboContext EnhancedComboContext { get; private set; }
        public CollectionViewSource GroupedItems { get; private set; }
        public SmartCollection<Item> Items { get; private set; }
        public IList<Toto> LinqGroup { get; private set; }

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

        private Item selectedItem;

        /// <summary>
        /// Title />
        /// </summary>
        private string title = "Poc combo améliorée";

        private void LoadItems()
        {
            Task.Run(() =>
            {
                List<Item> items = new List<Item>();
                foreach (var id in Enumerable.Range(1, 9999).Reverse())
                {
                    items.Add(new Item(id));
                }

                return items;
            })
            .ContinueWith(items =>
            {
                InvokeOnUIThread(() =>
                {
                    this.Items.Reset(items.Result);
                    this.LinqGroup = this.Items.GroupBy(x => x.RegroupementId)
                        .Select(x => new Toto()
                        {
                            Key = x.Key,
                            Items = x.ToList()
                        })
                        .ToList();

                    this.RaisePropertyChanged(nameof(LinqGroup));
                    //using (this.EnhancedComboContext.Items.DeferRefresh())
                    //{
                    //    foreach (var item in items.Result)
                    //    {
                    //        this.EnhancedComboContext.Items.AddNewItem(item);
                    //    }
                    //}
                    this.EnhancedComboContext = new Poc_ComboPlus.EnhancedComboContext(items.Result);
                    RaisePropertyChanged(nameof(EnhancedComboContext));
                });
            });
        }
    }

    public class Toto
    {
        public List<Item> Items { get; set; }
        public int Key { get; set; }
    }
}