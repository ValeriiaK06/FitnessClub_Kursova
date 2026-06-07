using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Models;
using FitnessClub.Services;

namespace FitnessClub.ViewModels;

[QueryProperty(nameof(ClientId), "client_id")]
public partial class ClientEditViewModel : BaseViewModel
{
    private readonly DatabaseService _db;
    private readonly INavigationService _nav;
    private readonly IDialogService _dialog;
    private Client? _editing;

    [ObservableProperty] private string lastName = string.Empty;
    [ObservableProperty] private string firstName = string.Empty;
    [ObservableProperty] private string middleName = string.Empty;
    [ObservableProperty] private DateTime birthDate = new(2000, 1, 1);
    [ObservableProperty] private string phone = string.Empty;
    [ObservableProperty] private string email = string.Empty;
    [ObservableProperty] private DateTime registrationDate = DateTime.Today;

    public ClientEditViewModel(DatabaseService db, INavigationService nav, IDialogService dialog)
    {
        _db = db;
        _nav = nav;
        _dialog = dialog;
    }

    private int _clientId;
    public int ClientId
    {
        get => _clientId;
        set { _clientId = value; Load(); }
    }

    private void Load()
    {
        if (_clientId == 0)
        {
            Title = "Новий клієнт";
            _editing = null;
            BirthDate = new DateTime(2000, 1, 1);
            RegistrationDate = DateTime.Today;
            return;
        }

        Title = "Редагувати клієнта";
        _editing = _db.GetClient(_clientId);

        if (_editing != null)
        {
            LastName = _editing.LastName;
            FirstName = _editing.FirstName;
            MiddleName = _editing.MiddleName;
            BirthDate = _editing.BirthDate;
            Phone = _editing.Phone;
            Email = _editing.Email;
            RegistrationDate = _editing.RegistrationDate;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(FirstName))
        {
            await _dialog.AlertAsync("Помилка", "Прізвище та ім'я обов'язкові", "OK");
            return;
        }

        if (_editing == null)
        {
            _db.AddClient(new Client
            {
                LastName = LastName,
                FirstName = FirstName,
                MiddleName = MiddleName ?? "",
                BirthDate = BirthDate,
                Phone = Phone ?? "",
                Email = Email ?? "",
                RegistrationDate = RegistrationDate
            });
        }
        else
        {
            _editing.LastName = LastName;
            _editing.FirstName = FirstName;
            _editing.MiddleName = MiddleName ?? "";
            _editing.BirthDate = BirthDate;
            _editing.Phone = Phone ?? "";
            _editing.Email = Email ?? "";
            _editing.RegistrationDate = RegistrationDate;
            _db.UpdateClient(_editing);
        }

        await _nav.GoBackAsync();
    }

    [RelayCommand]
    private async Task CancelAsync() => await _nav.GoBackAsync();
}