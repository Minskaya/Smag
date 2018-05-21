using System;
using System.Windows;

namespace DebugExpandingTextbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Public Constructors

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Private Methods

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Dummy.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.Dummy.IsOpen = true;
                aliceBlue.Focus();
            }));
        }

        #endregion Private Methods
    }
}