using FitnessClub.Views;
using Microsoft.Extensions.Logging;
using FitnessClub.Services;
using FitnessClub.ViewModels;



namespace FitnessClub
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<DatabaseService>();
            builder.Services.AddTransient<DashboardViewModel>();
            builder.Services.AddTransient<DashboardPage>();
            builder.Services.AddTransient<ScheduleViewModel>();
            builder.Services.AddTransient<SchedulePage>();
            builder.Services.AddTransient<ScheduleEditViewModel>();
            builder.Services.AddTransient<FitnessClub.Views.Details.ScheduleEditPage>();
            builder.Services.AddTransient<TrainersViewModel>();
            builder.Services.AddTransient<TrainersPage>();
            builder.Services.AddTransient<TrainerEditViewModel>();
            builder.Services.AddTransient<FitnessClub.Views.Details.TrainerEditPage>();
            builder.Services.AddTransient<ClientsViewModel>();
            builder.Services.AddTransient<ClientsPage>();
            builder.Services.AddTransient<ClientEditViewModel>();
            builder.Services.AddTransient<FitnessClub.Views.Details.ClientEditPage>();
            builder.Services.AddTransient<ClientSubsViewModel>();
            builder.Services.AddTransient<ClientSubsPage>();
            builder.Services.AddTransient<ClientSubEditViewModel>();
            builder.Services.AddTransient<FitnessClub.Views.Details.ClientSubEditPage>();
            builder.Services.AddTransient<SubscriptionHistoryViewModel>();
            builder.Services.AddTransient<SubscriptionHistoryPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
