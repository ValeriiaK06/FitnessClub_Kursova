namespace FitnessClub.Services
{
    public static class ThemeService
    {
        public static bool IsDark =>
            Application.Current?.UserAppTheme == AppTheme.Dark;

        public static void Toggle()
        {
            var app = Application.Current;
            if (app is null) return;

            // якщо Unspecified — орієнтуємось на поточну системну
            var current = app.UserAppTheme == AppTheme.Unspecified
                ? app.RequestedTheme
                : app.UserAppTheme;

            app.UserAppTheme = current == AppTheme.Dark
                ? AppTheme.Light
                : AppTheme.Dark;
        }
    }
}