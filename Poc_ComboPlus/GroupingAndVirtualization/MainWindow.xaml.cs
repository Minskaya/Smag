using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace GroupingAndVirtualization
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // The ListBox will bind to this collection view
        public FlatGroupListCollectionView View
        {
            get
            {
                if (_view == null)
                {
                    _view = new FlatGroupListCollectionView(Data);
                    _view.GroupDescriptions.Add(new PropertyGroupDescription("Type")); // Makes them group
                    _view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending)); // Just for looks
                }

                return _view;
            }
        }

        // This is the real data, but it will only be used by the collection view
        public ObservableCollection<TestData> Data
        {
            get
            {
                if (_data == null)
                {
                    var x = new List<TestData>()
                    {
                        new TestData("Apple", "Fruit"),
                        new TestData("Seattle", "City"),
                        new TestData("Orange", "Fruit"),
                        new TestData("Tacoma", "City"),

                        new TestData("Yakima", "City"),
                        new TestData("Spokane", "City"),
                        new TestData("Everett", "City"),
                        new TestData("Olympia", "City"),
                        new TestData("Portland", "City"),
                        new TestData("Salem", "City"),
                        new TestData("Eugene", "City"),
                        new TestData("San Francisco", "City"),
                        new TestData("San Jose", "City"),
                        new TestData("Sacramento", "City"),
                        new TestData("Monterey", "City"),
                        new TestData("Los Angeles", "City"),
                        new TestData("San Diego", "City"),
                        new TestData("Las Vegas", "City"),
                        new TestData("Salt Lake City", "City"),
                        new TestData("Phoenix", "City"),
                        new TestData("Flagstaff", "City"),
                        new TestData("Boise", "City"),

                        new TestData("Banana", "Fruit"),
                        new TestData("Pear", "Fruit"),
                        new TestData("Watermelon", "Fruit"),
                        new TestData("Strawberry", "Fruit"),
                        new TestData("Blueberry", "Fruit"),
                        new TestData("Raspberry", "Fruit"),
                        new TestData("Blackberry", "Fruit"),
                        new TestData("Mango", "Fruit"),
                        new TestData("Peach", "Fruit"),
                        new TestData("Cherry", "Fruit"),
                        new TestData("Grape", "Fruit"),
                        new TestData("Avocado", "Fruit"),
                        new TestData("Nextarine", "Fruit"),
                        new TestData("Lime", "Fruit"),
                        new TestData("Grapefruit", "Fruit"),
                        new TestData("Lemon", "Fruit"),
                        new TestData("Pineapple", "Fruit"),
                        new TestData("Apricot", "Fruit"),

                        // This list is actually too short for re-virtualization to kick in
                        // but if you lengthen it, you can start seeing the cleanup events.
                    };

                    var y = new List<TestData>();
                    for (int i = 0; i < 100; i++)
                    {
                        y.AddRange(x);
                    }

                    _data = new ObservableCollection<TestData>(y);
                }

                return _data;
            }
        }

        private ObservableCollection<TestData> _data;
        private FlatGroupListCollectionView _view;
    }
}