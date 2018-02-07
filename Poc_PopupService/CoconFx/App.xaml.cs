using System.Windows;

namespace CoconFx
{
	public partial class App : Application
	{
		void App_OnStartup(object sender, StartupEventArgs e)
		{
			/* The bootstrapper initializes the routes 
			 * for the navigation service. */
			var bootstrapper = new Bootstrapper();
			bootstrapper.Run();
		}
	}
}
