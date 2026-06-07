namespace FitnessClub.Services
{
    public class DialogService : IDialogService
    {
        public Task<bool> ConfirmAsync(string title, string message, string accept, string cancel)
            => Shell.Current.DisplayAlert(title, message, accept, cancel);

        public Task AlertAsync(string title, string message, string cancel)
            => Shell.Current.DisplayAlert(title, message, cancel);
    }
}