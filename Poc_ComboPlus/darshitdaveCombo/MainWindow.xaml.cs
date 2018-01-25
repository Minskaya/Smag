using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace darshitdaveCombo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string FavouritesGroupName = "Favourites";
        private const string HistoryGroupName = "History";
        private const string MRUGroupName = "Most Recently Used";
        private ObservableCollection<URLDetails> _URLs;

        public MainWindow()
        {
            _URLs = new ObservableCollection<URLDetails>();
            InitializeComponent();
            this.DataContext = this;
            FillDummyData();
        }

        public int NumberOfItems
        {
            get
            {
                return 3;
            }
        }

        public ObservableCollection<URLDetails> URLs
        {
            get { return _URLs; }
        }

        private void FillDummyData()
        {
            // Add MRU link
            _URLs.Add(new URLDetails { GroupName = MRUGroupName, Link = "www.msdn.com" });

            // Add History links
            _URLs.Add(new URLDetails { GroupName = HistoryGroupName, Link = "www.google.co.in" });
            _URLs.Add(new URLDetails { GroupName = HistoryGroupName, Link = "www.yahoo.com" });
            _URLs.Add(new URLDetails { GroupName = HistoryGroupName, Link = "www.msn.com" });
            _URLs.Add(new URLDetails { GroupName = HistoryGroupName, Link = "https://mail.google.com" });
            _URLs.Add(new URLDetails { GroupName = HistoryGroupName, Link = "www.msdn.com" });
            _URLs.Add(new URLDetails { GroupName = HistoryGroupName, Link = "www.wikipedia.com" });

            // Add Favorite links
            _URLs.Add(new URLDetails { GroupName = FavouritesGroupName, Link = "www.mymail.com" });
            _URLs.Add(new URLDetails { GroupName = FavouritesGroupName, Link = "www.codeproject.com" });
            _URLs.Add(new URLDetails { GroupName = FavouritesGroupName, Link = "www.songs.in" });
        }
    }
}