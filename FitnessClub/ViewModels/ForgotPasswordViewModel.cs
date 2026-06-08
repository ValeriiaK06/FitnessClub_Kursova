using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Services;

namespace FitnessClub.ViewModels
{
    public partial class ForgotPasswordViewModel : BaseViewModel
    {
        private readonly AuthService _auth;
        private readonly IDialogService _dialog;
       

        [ObservableProperty] private string question = string.Empty;
        [ObservableProperty] private string answer = string.Empty;
        [ObservableProperty] private string newPassword = string.Empty;

        // Чи підтверджена відповідь (тоді показуємо поле нового пароля)
        [ObservableProperty] private bool isAnswerConfirmed;

        public ForgotPasswordViewModel(AuthService auth, IDialogService dialog)
        {
            _auth = auth;
            _dialog = dialog;
            Title = "Відновлення пароля";
            Question = _auth.GetSecurityQuestion();
        }

        [RelayCommand]
        private async Task ConfirmAnswerAsync()
        {
            if (string.IsNullOrWhiteSpace(Answer))
            {
                await _dialog.AlertAsync("Помилка", "Введіть відповідь", "OK");
                return;
            }

            if (_auth.CheckSecurityAnswer(Answer))
            {
                IsAnswerConfirmed = true;   // показуємо поле нового пароля
            }
            else
            {
                await _dialog.AlertAsync("Помилка", "Невірна відповідь на секретне питання", "OK");
            }
        }

       

        [RelayCommand]
        private async Task SaveNewPasswordAsync()
        {
            if (string.IsNullOrWhiteSpace(NewPassword) || NewPassword.Length < 4)
            {
                await _dialog.AlertAsync("Помилка", "Новий пароль має містити щонайменше 4 символи", "OK");
                return;
            }

            _auth.ChangePassword(NewPassword);
            await _dialog.AlertAsync("Готово", "Пароль змінено. Тепер увійдіть з новим паролем.", "OK");
            BackToLogin();
        }

        [RelayCommand]
        private void Cancel() => BackToLogin();

        private void BackToLogin()
        {
            var services = Application.Current!.Windows[0].Page!.Handler!.MauiContext!.Services;
            var page = services.GetService<Views.LoginPage>();
            if (page != null)
                Application.Current!.Windows[0].Page = page;
        }

       
    }
}