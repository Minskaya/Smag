using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ObjectInCell
{
    public class MyViewModel : Telerik.Windows.Controls.ViewModelBase
    {
        public MyViewModel()
        {
            this.ItemsSource = new ObservableCollection<Row>()
            {
                new Row() { RowName = "Row 1" },
                new Row() { RowName = "Row 2" },
                new Row() { RowName = "Row 3" },
            };
        }

        public ObservableCollection<Row> ItemsSource { get; set; }
    }

    public class Row
    {
        public Row()
        {
            this.Item = new Item();
        }

        public Item Item { get; private set; }
        public string RowName { get; set; }
    }

    public class Item
    {
        private static Random rnd = new Random();

        public Item()
        {
            this.Display = string.Format("Item {0}", rnd.Next(1, 999));
            this.FireItem = new Telerik.Windows.Controls.DelegateCommand((object o) =>
            {
                System.Diagnostics.Debug.WriteLine("Double click on " + this.Display);
            });
        }

        public string Display { get; set; }

        public ICommand FireItem { get; set; }
    }
}