using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FitnessClub.Services;

namespace FitnessClub.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly AuthService _auth;
        private readonly IDialogService _dialog;

        [ObservableProperty] private string login = string.Empty;
        [ObservableProperty] private string password = string.Empty;

        // Поки база не завантажилась — вхід заблоковано
        [ObservableProperty] private bool isReady;
        [ObservableProperty] private string statusText = "Завантаження даних...";

        public LoginViewModel(AuthService auth, IDialogService dialog)
        {
            _auth = auth;
            _dialog = dialog;
            Title = "Вхід";

            // Чекаємо сигналу, що база завантажилась з хмари
            WeakReferenceMessenger.Default.Register<DataSyncedMessage>(this, (r, m) =>
            {
                IsReady = true;
                StatusText = string.Empty;
            });
        }

        [RelayCommand]
        private async Task SignInAsync()
        {
            if (!IsReady)
            {
                await _dialog.AlertAsync("Зачекайте", "Дані ще завантажуються, спробуйте за мить", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
            {
                await _dialog.AlertAsync("Помилка", "Введіть логін і пароль", "OK");
                return;
            }

            if (_auth.Validate(Login.Trim(), Password))
            {
                Application.Current!.Windows[0].Page = new AppShell();
            }
            else
            {
                await _dialog.AlertAsync("Помилка", "Невірний логін або пароль", "OK");
            }
        }
    }
}