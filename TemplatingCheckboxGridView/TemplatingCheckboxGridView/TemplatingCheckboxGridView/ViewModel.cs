using System.Collections.ObjectModel;
using System.Linq;

namespace TemplatingCheckboxGridView
{
    public class ViewModel
    {
        public ViewModel()
        {
            this.Items = new ObservableCollection<Item>()
            {
                new Item("aaaa") { IsSelected = true },
                new Item("bbbb"),
            };
        }

        public ObservableCollection<Item> Items { get; private set; }

        public Item FirstCheck
        {
            get
            {
                return this.Items.First();
            }
        }
    }

    public class Item : Telerik.Windows.Controls.ViewModelBase
    {
        public Item(string name)
        {
            _Name = name;
        }

        /// <summary>
        /// Name />
        /// </summary>
        private string _Name;

        /// <summary>
        /// Gets and sets The property's value
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
                this.OnPropertyChanged(nameof(_Name));
            }
        }

        /// <summary>
        /// isSelected />
        /// </summary>
        private bool _isSelected;

        /// <summary>
        /// Gets and sets The property's value
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }

            set
            {
                _isSelected = value;
                this.OnPropertyChanged(nameof(IsSelected));
            }
        }
    }
}