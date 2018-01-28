using System.Collections.ObjectModel;
using System.Windows;

namespace darshitdaveCombo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class daveWindow : Window
    {
        private const string FavouritesGroupName = "Favourites";
        private const string HistoryGroupName = "History";
        private const string MRUGroupName = "Most Recently Used";
        private ObservableCollection<URLDetails> _URLs;

        public daveWindow()
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
                return 0;
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