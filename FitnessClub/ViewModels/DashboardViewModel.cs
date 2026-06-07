using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Services;

namespace FitnessClub.ViewModels
{
    public partial class DashboardViewModel : BaseViewModel
    {
        private readonly DatabaseService _db;

        [ObservableProperty] private int trainersCount;
        [ObservableProperty] private int clientsCount;
        [ObservableProperty] private int subscriptionsCount;
        [ObservableProperty] private int bookingsCount;
        [ObservableProperty] private int activeSubsCount;
        [ObservableProperty] private int schedulesCount;

        public DashboardViewModel(DatabaseService db)
        {
            _db = db;
            Title = "Дашборд";
        }

        [RelayCommand]
        private void Load()
        {
            TrainersCount = _db.GetTrainers().Count;
            ClientsCount = _db.GetClients().Count;
            SubscriptionsCount = _db.GetSubscriptions().Count;
            BookingsCount = _db.GetActiveBookingsCount();
            SchedulesCount = _db.GetSchedules().Count;
            ActiveSubsCount = _db.GetClientSubscriptions().Count(s => s.IsActive);
        }
    }
}