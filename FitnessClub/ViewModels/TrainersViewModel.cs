using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Models;
using FitnessClub.Services;
using System.Collections.ObjectModel;

namespace FitnessClub.ViewModels;

public partial class TrainersViewModel : BaseViewModel
{
    private readonly DatabaseService _db;
    private readonly INavigationService _nav;
    private readonly IDialogService _dialog;

    [ObservableProperty]
    private ObservableCollection<Trainer> trainers = new();

    public TrainersViewModel(DatabaseService db, INavigationService nav, IDialogService dialog)
    {
        _db = db;
        _nav = nav;
        _dialog = dialog;
        Title = "Тренери";
    }

    [RelayCommand]
    private void Load()
    {
        var data = _db.GetTrainers();
        Trainers.Clear();
        foreach (var t in data) Trainers.Add(t);
    }

    [RelayCommand]
    private async Task AddAsync() => await _nav.GoToAsync("traineredit?trainer_id=0");

    [RelayCommand]
    private async Task EditAsync(int id) => await _nav.GoToAsync($"traineredit?trainer_id={id}");

  
    [RelayCommand]
    private async Task DeleteAsync(int id)
    {
        // Перевіряємо, чи має тренер заняття
        int scheduleCount = _db.CountSchedulesForTrainer(id);
        if (scheduleCount > 0)
        {
            await _dialog.AlertAsync(
                "Неможливо видалити",
                $"У цього тренера є заняття в розкладі ({scheduleCount}). " +
                "Спершу видмініть або перенесіть його заняття.",
                "OK");
            return;
        }

        bool confirm = await _dialog.ConfirmAsync(
            "Видалення", "Видалити цього тренера?", "Так", "Скасувати");
        if (!confirm) return;

        var item = _db.GetTrainer(id);
        if (item != null)
        {
            _db.DeleteTrainer(item);
            Load();
        }
    }
}