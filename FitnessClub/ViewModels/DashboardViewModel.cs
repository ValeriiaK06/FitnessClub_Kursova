using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FitnessClub.Services;

namespace FitnessClub.ViewModels
{
    public partial class DashboardViewModel : BaseViewModel
    {
        private readonly DatabaseService _db;
        private readonly IDialogService _dialog;
        private readonly AuthService _auth;

       
        [ObservableProperty] private int trainersCount;
        [ObservableProperty] private int clientsCount;
        [ObservableProperty] private int subscriptionsCount;
        [ObservableProperty] private int bookingsCount;
        [ObservableProperty] private int activeSubsCount;
        [ObservableProperty] private int schedulesCount;

        [ObservableProperty] private string adminName = "Адміністратор";
        [ObservableProperty] private bool isEditingName;
        [ObservableProperty] private string editNameText = string.Empty;

        // Зміна пароля
        [ObservableProperty] private bool isChangingPassword;
        [ObservableProperty] private string oldPassword = string.Empty;
        [ObservableProperty] private string newPassword = string.Empty;

        public DashboardViewModel(DatabaseService db, IDialogService dialog, AuthService auth)
        {
            _db = db;
            _dialog = dialog;
            _auth = auth;
            Title = "Дашборд";

            WeakReferenceMessenger.Default.Register<DataSyncedMessage>(this, (r, m) =>
            {
                Load();
            });
        }

        [RelayCommand]
        private void Load()
        {
            AdminName = _auth.GetAdminName();
            TrainersCount = _db.GetTrainers().Count;
            ClientsCount = _db.GetClients().Count;
            SubscriptionsCount = _db.GetSubscriptions().Count;
            BookingsCount = _db.GetActiveBookingsCount();
            SchedulesCount = _db.GetSchedules().Count;
            ActiveSubsCount = _db.GetClientSubscriptions().Count(s => !s.IsExpired);
        }
        // ===== ІМ'Я АДМІНА =====
        [RelayCommand]
        private void StartEditName()
        {
            EditNameText = AdminName;
            IsEditingName = true;
        }

       
        [RelayCommand]
        private void SaveName()
        {
            var trimmed = EditNameText?.Trim();
            if (!string.IsNullOrEmpty(trimmed))
            {
                AdminName = trimmed;
                _auth.SetAdminName(trimmed);
            }
            IsEditingName = false;
        }

        [RelayCommand]
        private void CancelEditName()
        {
            IsEditingName = false;
        }

        // ===== ЗМІНА ПАРОЛЯ =====
        [RelayCommand]
        private void StartChangePassword()
        {
            OldPassword = string.Empty;
            NewPassword = string.Empty;
            IsChangingPassword = true;
        }

        [RelayCommand]
        private async Task SavePasswordAsync()
        {
            if (!_auth.CheckPassword(OldPassword))
            {
                await _dialog.AlertAsync("Помилка", "Поточний пароль невірний", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(NewPassword) || NewPassword.Length < 4)
            {
                await _dialog.AlertAsync("Помилка", "Новий пароль має містити щонайменше 4 символи", "OK");
                return;
            }

            _auth.ChangePassword(NewPassword);
            IsChangingPassword = false;
            await _dialog.AlertAsync("Готово", "Пароль успішно змінено", "OK");
        }

        [RelayCommand]
        private void CancelChangePassword()
        {
            IsChangingPassword = false;
        }
    }
}