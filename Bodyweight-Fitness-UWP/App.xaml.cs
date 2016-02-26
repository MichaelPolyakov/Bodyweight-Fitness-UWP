using Windows.UI.Xaml;
using System.Threading.Tasks;
using Bodyweight_Fitness_UWP.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Template10.Controls;
using Template10.Common;

namespace Bodyweight_Fitness_UWP
{
    // documentation on the bootstrapper
    // https://github.com/Windows-XAML/Template10/wiki/Docs-%7C-Bootstrapper

    sealed partial class App : Template10.Common.BootStrapper
    {
        public App()
        {
            InitializeComponent();
            SplashFactory = (e) => new Views.Splash(e);

            #region App settings

            var _settings = SettingsService.Instance;
            RequestedTheme = _settings.AppTheme;
            CacheMaxDuration = _settings.CacheMaxDuration;
            ShowShellBackButton = _settings.UseShellBackButton;

            #endregion
        }

        public override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            // content may already be shell when resuming
            if ((Window.Current.Content as ModalDialog) == null)
            {
                // setup hamburger shell, wrapped in a modal dialog
                var nav = NavigationServiceFactory(BackButton.Attach, ExistingContent.Include);
                Window.Current.Content = new ModalDialog
                {
                    DisableBackButtonWhenModal = true,
                    Content = new Views.Shell(nav),
                    ModalContent = new Views.Busy(),
                };
            }
            return Task.CompletedTask;
        }

        public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            // long-running startup tasks go here

            NavigationService.Navigate(typeof(Views.MainPage));
            return Task.CompletedTask;
        }
    }
}

