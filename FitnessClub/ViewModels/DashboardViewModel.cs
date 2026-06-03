using CommunityToolkit.Mvvm.ComponentModel;
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

        public void Refresh()
        {
            TrainersCount = _db.GetTrainers().Count;
            ClientsCount = _db.GetClients().Count;
            SubscriptionsCount = _db.GetSubscriptions().Count;
            BookingsCount = _db.GetBookings().Count;
            SchedulesCount = _db.GetSchedules().Count;
            ActiveSubsCount = _db.GetClientSubscriptions().Count(s => s.IsActive);
        }
    }
}