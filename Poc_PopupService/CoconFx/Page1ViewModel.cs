using System.Threading.Tasks;
using System.Windows.Input;

using Codon.Messaging;
using Codon.Services;
using Codon.UIModel;
using Codon.UIModel.Input;

namespace CoconFx
{
    /// <summary>
    /// This class is a the ViewModel for a page
    /// and demonstrates the use of commands,
    /// the dialog service, the pub/sub messaging system,
    /// the navigation service, and the settings service.
    /// </summary>
    public class Page1ViewModel : ViewModelBase,
        IMessageSubscriber<ExampleMessage>
    {
        private readonly IDialogService dialogService;

        private readonly INavigationService navigationService;

        private readonly ISettingsService settingsService;

        private int creationCount;

        private string foo;

        private ActionCommand navigateToPage2Command;

        private ActionCommand publishMessageCommand;

        private ActionCommand showDialogCommand;

        /// <summary>
        /// The IoC container uses dependency injection
        /// to pass instances of the specified services.
        /// </summary>
        /// <param name="dialogService">
        /// The cross-platform dialog service is used
        /// to display messages and ask the user questions.</param>
        /// <param name="navigationService">
        /// The cross-platform navigation service allows navigation
        /// to named pages within the app and relies
        /// on the <see cref="Codon.Navigation.IRoutingService"/>.
        /// See the <c>Bootstrapper</c> class in the platform specific
        /// apps (e.g., CoconFx.Uwp) to explore how the routing service
        /// is configured.</param>
        /// <param name="settingsService">
        /// Provides cross-platform transient, roaming,
        /// and persistant storage of keyed values.
        /// </param>
        public Page1ViewModel(
            IDialogService dialogService,
            INavigationService navigationService,
            ISettingsService settingsService)
        {
            this.dialogService = dialogService;
            //?? throw new ArgumentNullException(nameof(dialogService));
            this.navigationService = navigationService;
            //?? throw new ArgumentNullException(nameof(navigationService));
            this.settingsService = settingsService;
            //?? throw new ArgumentNullException(nameof(settingsService));

            UpdateCreationCount();
        }

        /// <summary>
        /// This property is set via the <see cref="UpdateCreationCount"/>
        /// method. It reflects the value that is retrieved, incremented,
        /// and stored via the settings service.
        /// </summary>
        public int CreationCount
        {
            get { return creationCount; }
            private set { Set(ref creationCount, value); }
        }

        /// <summary>
        /// This property demonstrates how to and raise
        /// <see cref="INotifyPropertyChanging"/>
        /// and <see cref="INotifyPropertyChanged"/>
        /// using the base class's <c>Set</c> method.
        /// Property change events are automatically raised
        /// on the UI thread. </summary>
        public string Foo
        {
            get { return foo; }
            set { Set(ref foo, value); }
        }

        public ICommand NavigateToPage2Command => navigateToPage2Command
                    ?? (navigateToPage2Command = new ActionCommand(NavigateToPage2));

        public ICommand PublishMessageCommand => publishMessageCommand
                    ?? (publishMessageCommand = new ActionCommand(PublishMessage));

        public ICommand ShowDialogCommand => showDialogCommand
                    ?? (showDialogCommand = new ActionCommand(ShowDialog));

        /// <summary>
        /// This method is called by the <see cref="navigateToPage2Command"/>
        /// when a button is activated in the view.
        /// </summary>
        private void NavigateToPage2(object arg)
        {
            navigationService.Navigate(Routes.Page2);

            /* You can pass parameters by using the Navigate overload, like so:
             * navigationService.Navigate(Routes.Page2, "Message from Page1ViewModel");
             *
             * And then retrieve the value using:
             * object argumentPassedFromPage1 = navigationService.NavigationArgument;
             */
        }

        /// <summary>
        /// This method is called by the <see cref="publishMessageCommand"/>
        /// when a button is activated in the view.
        /// </summary>
        private void PublishMessage(object arg)
        {
            Messenger.PublishAsync(new ExampleMessage(), true);
        }

        /// <summary>
        /// This method is called by the <see cref="showDialogCommand"/>
        /// when a button is activated in the view.
        /// </summary>
        private void ShowDialog(object arg)
        {
            dialogService.ShowMessageAsync("This is a sample message.", "Message from Sample 1");
        }

        private void UpdateCreationCount()
        {
            const string key = "Page1CreationCount";
            /* Use the settings service to retrieve, increment,
             * and store a counter.
             * The settings service is cross-platform. */
            int count = settingsService.GetSetting(key, 0);
            settingsService.SetSetting(key, ++count);

            CreationCount = count;
        }

        /// <summary>
        /// <c>Page1ViewModel</c> receives <c>ExampleMessage</c> messages.
        /// It subscribes to the message by implementing
        /// <c>IMessageSubscriber&lt;ExampleMessage&gt;</c>
        /// and by calling <c>Messenger.Subscribe(this)</c>
        /// in the <c>ViewModelBase</c> constructor.
        /// When the <see cref="PublishMessage"/> method is called,
        /// the messenger is used to dispatch an <c>ExampleMessage</c> object.
        /// This method (<c>ReceiveMessageAsync</c>) is then called.
        /// </summary>
        /// <param name="message">The message to receive.</param>
        public Task ReceiveMessageAsync(ExampleMessage message)
        {
            dialogService.ShowMessageAsync("Received the message.", "Message from App");

            return Task.FromResult<object>(null);
        }
    }
}