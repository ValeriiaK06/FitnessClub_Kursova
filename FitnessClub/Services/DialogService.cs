namespace FitnessClub.Services
{
    public class DialogService : IDialogService
    {
        private Page? CurrentPage =>
            Shell.Current ?? Application.Current?.Windows.FirstOrDefault()?.Page;

        public Task<bool> ConfirmAsync(string title, string message, string accept, string cancel)
            => CurrentPage!.DisplayAlert(title, message, accept, cancel);

        public Task AlertAsync(string title, string message, string cancel)
            => CurrentPage!.DisplayAlert(title, message, cancel);
    }
}