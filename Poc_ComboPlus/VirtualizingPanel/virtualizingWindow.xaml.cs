using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace VirtualizingPanel
{
    public class Person
    {
        public Person(int id)
        {
            Age = id;
            Name = $"Name {id}";
        }

        public int Age { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class virtualizingWindow : Window, INotifyPropertyChanged
    {
        public virtualizingWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Persons = new ObservableCollection<Person>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Message { get; set; }
        public ObservableCollection<Person> Persons { get; private set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var id in Enumerable.Range(1, 500))
            {
                this.Persons.Add(new Person(id));
            }

            var watch = new Stopwatch();
            watch.Start();
            //Wait for the rendering is finished..
            Dispatcher.CurrentDispatcher.Invoke(
                new Action(() => { }),
                DispatcherPriority.Loaded, null);

            watch.Stop();

            Message = string.Format("Rendering took {0} ms.", watch.ElapsedMilliseconds);
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Message)));
        }
    }
}