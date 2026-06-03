using CommunityToolkit.Mvvm.ComponentModel; // Обязательно для [ObservableProperty]
using CommunityToolkit.Mvvm.Input;         // Обязательно для [RelayCommand]
using FitnessClub.Models;
using FitnessClub.Services;
using System.Collections.ObjectModel;

namespace FitnessClub.ViewModels;

// 1. ДОБАВЛЕНО СЛОВО partial!
public partial class ScheduleViewModel : BaseViewModel
{
    private readonly DatabaseService _databaseService;

    // 2. ИСПРАВЛЕНА НЕОДНОЗНАЧНОСТЬ: 
    // Пишем с маленькой буквы и добавляем атрибут. 
    // Система сама создаст публичное свойство Schedules с большой буквы.
    [ObservableProperty]
    private ObservableCollection<Schedule> _schedules = new();

    public ScheduleViewModel()
    {
        _databaseService = new DatabaseService();
    }

    // 3. ДОБАВЛЕНА КОМАНДА:
    // Атрибут [RelayCommand] автоматически сгенерирует свойство "LoadCommand", 
    // которое ищет ваша XAML-страница.
    [RelayCommand]
    public void Load()
    {
        var dbSchedules = _databaseService.GetSchedules();

        Schedules.Clear();
        foreach (var schedule in dbSchedules)
        {
            Schedules.Add(schedule);
        }
    }
}