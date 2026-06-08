using CommunityToolkit.Mvvm.Messaging;
using FitnessClub.Services;

namespace FitnessClub
{
    public partial class App : Application
    {
        private readonly SupabaseSyncService _sync;

        public App(SupabaseSyncService sync)
        {
            InitializeComponent();
            UserAppTheme = AppTheme.Dark;
            _sync = sync;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            // Стартуємо з екрана входу
            var loginPage = Handler?.MauiContext?.Services.GetService<Views.LoginPage>()
                            ?? throw new InvalidOperationException("LoginPage не зареєстровано");

            var window = new Window(loginPage);

            window.Created += async (s, e) =>
            {
                await _sync.DownloadAsync();
                WeakReferenceMessenger.Default.Send(new DataSyncedMessage());
            };

            return window;
        }
    }
}