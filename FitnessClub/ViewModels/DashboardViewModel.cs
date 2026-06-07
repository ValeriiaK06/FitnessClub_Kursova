using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Services;

namespace FitnessClub.ViewModels
{
    public partial class DashboardViewModel : BaseViewModel
    {
        private readonly DatabaseService _db;
        private readonly IDialogService _dialog;

        private const string AdminNameKey = "admin_name";

        [ObservableProperty] private int trainersCount;
        [ObservableProperty] private int clientsCount;
        [ObservableProperty] private int subscriptionsCount;
        [ObservableProperty] private int bookingsCount;
        [ObservableProperty] private int activeSubsCount;
        [ObservableProperty] private int schedulesCount;

        // Ім'я адміністратора
        [ObservableProperty] private string adminName = "Адміністратор";

        // Режим редагування імені
        [ObservableProperty] private bool isEditingName;
        [ObservableProperty] private string editNameText = string.Empty;

        public DashboardViewModel(DatabaseService db, IDialogService dialog)
        {
            _db = db;
            _dialog = dialog;
            Title = "Дашборд";
        }

        [RelayCommand]
        private void Load()
        {
            // Завантажуємо збережене ім'я (або значення за замовчуванням)
            AdminName = Preferences.Get(AdminNameKey, "Адміністратор");

            TrainersCount = _db.GetTrainers().Count;
            ClientsCount = _db.GetClients().Count;
            SubscriptionsCount = _db.GetSubscriptions().Count;
            BookingsCount = _db.GetActiveBookingsCount();
            SchedulesCount = _db.GetSchedules().Count;
            ActiveSubsCount = _db.GetClientSubscriptions().Count(s => s.IsActive);
        }

        // Почати редагування — відкриває поле з поточним ім'ям
        [RelayCommand]
        private void StartEditName()
        {
            EditNameText = AdminName;
            IsEditingName = true;
        }

        // Зберегти нове ім'я
        [RelayCommand]
        private void SaveName()
        {
            var trimmed = EditNameText?.Trim();
            if (!string.IsNullOrEmpty(trimmed))
            {
                AdminName = trimmed;
                Preferences.Set(AdminNameKey, trimmed);   // зберігаємо між запусками
            }
            IsEditingName = false;
        }

        // Скасувати
        [RelayCommand]
        private void CancelEditName()
        {
            IsEditingName = false;
        }
    }
}