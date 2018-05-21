using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace MultiBinding
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            // Positionne la culture du xaml
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(
                        CultureInfo.CurrentUICulture.IetfLanguageTag)));
        }
    }
}