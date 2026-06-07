using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Models;
using FitnessClub.Services;


namespace FitnessClub.ViewModels;

[QueryProperty(nameof(TrainerId), "trainer_id")]
public partial class TrainerEditViewModel : BaseViewModel
{
    private readonly DatabaseService _db;
    private readonly INavigationService _nav;
    private readonly IDialogService _dialog;
    private Trainer? _editing;

    [ObservableProperty] private string lastName = string.Empty;
    [ObservableProperty] private string firstName = string.Empty;
    [ObservableProperty] private string middleName = string.Empty;
    [ObservableProperty] private string specialization = string.Empty;
    [ObservableProperty] private string experience = string.Empty;
    [ObservableProperty] private string photo = string.Empty;
    [ObservableProperty] private string phone = string.Empty;
    [ObservableProperty] private string email = string.Empty;

    public TrainerEditViewModel(DatabaseService db, INavigationService nav, IDialogService dialog)
    {
        _db = db;
        _nav = nav;
        _dialog = dialog;
    }

    private int _trainerId;
    public int TrainerId
    {
        get => _trainerId;
        set { _trainerId = value; Load(); }
    }

    private void Load()
    {
        if (_trainerId == 0)
        {
            Title = "Новий тренер";
            _editing = null;
            return;
        }

        Title = "Редагувати тренера";
        _editing = _db.GetTrainer(_trainerId);

        if (_editing != null)
        {
            LastName = _editing.LastName;
            FirstName = _editing.FirstName;
            MiddleName = _editing.MiddleName;
            Specialization = _editing.Specialization;
            Experience = _editing.Experience.ToString();
            Photo = _editing.Photo;
            Phone = _editing.Phone;
            Email = _editing.Email;
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

        int.TryParse(Experience, out int exp);

        if (_editing == null)
        {
            _db.AddTrainer(new Trainer
            {
                LastName = LastName,
                FirstName = FirstName,
                MiddleName = MiddleName ?? "",
                Specialization = Specialization ?? "",
                Experience = exp,
                Photo = Photo ?? "",
                Phone = Phone ?? "",
                Email = Email ?? ""
            });
        }
        else
        {
            _editing.LastName = LastName;
            _editing.FirstName = FirstName;
            _editing.MiddleName = MiddleName ?? "";
            _editing.Specialization = Specialization ?? "";
            _editing.Experience = exp;
            _editing.Photo = Photo ?? "";
            _editing.Phone = Phone ?? "";
            _editing.Email = Email ?? "";
            _db.UpdateTrainer(_editing);
        }

        await _nav.GoBackAsync();
    }

    [RelayCommand]
    private async Task CancelAsync() => await _nav.GoBackAsync();
}