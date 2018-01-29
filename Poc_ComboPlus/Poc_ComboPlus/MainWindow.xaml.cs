using System.Windows;
using Telerik.Windows.Controls;

namespace Poc_ComboPlus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            StyleManager.ApplicationTheme = new Windows8Theme();
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}