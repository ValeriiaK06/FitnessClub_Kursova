using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Models;
using FitnessClub.Services;
using System.Collections.ObjectModel;

namespace FitnessClub.ViewModels;

public partial class ClientSubsViewModel : BaseViewModel
{
    private readonly DatabaseService _db;

    [ObservableProperty]
    private ObservableCollection<ClientSubscription> items = new();

    public ClientSubsViewModel(DatabaseService db)
    {
        _db = db;
        Title = "Абонементи клієнтів";
    }

    [RelayCommand]
    private void Load()
    {
        var data = _db.GetClientSubscriptions();
        Items.Clear();
        foreach (var x in data)
            Items.Add(x);
    }

    [RelayCommand]
    private async Task AddAsync()
    {
        await Shell.Current.GoToAsync("clientsubedit?clientsub_id=0");
    }

    [RelayCommand]
    private async Task EditAsync(int id)
    {
        await Shell.Current.GoToAsync($"clientsubedit?clientsub_id={id}");
    }

    [RelayCommand]
    private async Task DeleteAsync(int id)
    {
        bool confirm = await Shell.Current.DisplayAlert(
            "Видалення", "Видалити цей абонемент клієнта?", "Так", "Скасувати");
        if (!confirm) return;

        var item = _db.GetClientSubscription(id);
        if (item != null)
        {
            _db.DeleteClientSubscription(item);
            Load();
        }
    }
}