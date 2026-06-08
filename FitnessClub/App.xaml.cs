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
            // Тимчасова сторінка-заставка із завантаженням
            var loadingPage = new ContentPage
            {
                Content = new VerticalStackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Spacing = 16,
                    Children =
                    {
                        new ActivityIndicator { IsRunning = true, HorizontalOptions = LayoutOptions.Center },
                        new Label { Text = "Завантаження даних...", HorizontalOptions = LayoutOptions.Center }
                    }
                }
            };

            var window = new Window(loadingPage);

            // Качаємо базу, тоді показуємо вхід
            window.Created += async (s, e) =>
            {
                await _sync.DownloadAsync();

                var loginPage = Handler?.MauiContext?.Services.GetService<Views.LoginPage>();
                if (loginPage != null)
                    window.Page = loginPage;
            };

            return window;
        }
    }
}