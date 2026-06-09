using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Models;
using FitnessClub.Services;
using System.Collections.ObjectModel;

namespace FitnessClub.ViewModels;

public partial class ScheduleViewModel : BaseViewModel
{
    private readonly DatabaseService _db;
    private readonly INavigationService _nav;
    private readonly IDialogService _dialog;

    [ObservableProperty]
    private ObservableCollection<Schedule> schedules = new();

    public ScheduleViewModel(DatabaseService db, INavigationService nav, IDialogService dialog)
    {
        _db = db;
        _nav = nav;
        _dialog = dialog;
        Title = "Розклад";
    }

    [RelayCommand]
    private void Load()
    {
        var data = _db.GetSchedules();
        Schedules.Clear();
        foreach (var s in data)
            Schedules.Add(s);
    }

    [RelayCommand]
    private async Task AddAsync()
        => await _nav.GoToAsync("scheduleedit?schedule_id=0");

    [RelayCommand]
    private async Task EditAsync(int id)
        => await _nav.GoToAsync($"scheduleedit?schedule_id={id}");

    [RelayCommand]
    private async Task DeleteAsync(int id)
    {
       
        int activeBookings = _db.CountActiveBookingsForSchedule(id);
        if (activeBookings > 0)
        {
            await _dialog.AlertAsync(
                "Неможливо видалити",
                $"На це заняття є активні записи клієнтів ({activeBookings}). " +
                "Спершу скасуйте ці записи, а потім видаляйте заняття.",
                "OK");
            return;
        }

        bool confirm = await _dialog.ConfirmAsync(
            "Видалення", "Видалити це заняття з розкладу?", "Так", "Скасувати");
        if (!confirm) return;

        var item = _db.GetSchedule(id);
        if (item != null)
        {
            _db.DeleteSchedule(item);
            Load();
        }
    }
}