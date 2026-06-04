using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Models;
using FitnessClub.Services;
using System.Collections.ObjectModel;

namespace FitnessClub.ViewModels;

public partial class ScheduleViewModel : BaseViewModel
{
    private readonly DatabaseService _db;

    [ObservableProperty]
    private ObservableCollection<Schedule> schedules = new();

    // DI передає той самий singleton DatabaseService
    public ScheduleViewModel(DatabaseService db)
    {
        _db = db;
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
    {
        await Shell.Current.GoToAsync("scheduleedit?schedule_id=0");
    }

    [RelayCommand]
    private async Task EditAsync(int id)
    {
        await Shell.Current.GoToAsync($"scheduleedit?schedule_id={id}");
    }

    [RelayCommand]
    private async Task DeleteAsync(int id)
    {
        bool confirm = await Shell.Current.DisplayAlert(
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