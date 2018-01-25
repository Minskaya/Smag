using System.ComponentModel;
using System.Linq;
using Telerik.Windows.Data;

namespace Poc_ComboPlus
{
    public class EnhancedComboContext : INotifyPropertyChanged
    {
        private QueryableCollectionView _Items;

        public QueryableCollectionView Items
        {
            get
            {
                if (_Items == null)
                {
                    //_Items = new QueryableCollectionView(from i in Enumerable.Range(0, 10000)
                    //                                     select new MyObject() { ID = i, Name = String.Format("Name{0}", i) });

                    _Items = new QueryableCollectionView(Enumerable.Empty<Item>());
                    _Items.FilterDescriptors.Add(
                        new FilterDescriptor(
                            "Name",
                            FilterOperator.Contains,
                            _Text != null
                            ? _Text
                            : ""));
                }

                return _Items;
            }
        }

        private string _Text;

        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                if (_Text != value)
                {
                    _Text = value;

                    if (_Items != null)
                    {
                        ((FilterDescriptor)_Items.FilterDescriptors[0]).Value = _Text;
                    }

                    OnPropertyChanged("Text");
                }
            }
        }

        private Item _SelectedItem;

        public Item SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;

                    _Text = _SelectedItem.Name;

                    OnPropertyChanged("Text");

                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        #region INotifyPropertyChanged Members

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}