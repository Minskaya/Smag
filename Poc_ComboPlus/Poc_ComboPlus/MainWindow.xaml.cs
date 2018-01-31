using System;
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
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                // Log error (including InnerExceptions!)
                // Handle exception
            }
            this.DataContext = new MainWindowViewModel();
        }
    }
}