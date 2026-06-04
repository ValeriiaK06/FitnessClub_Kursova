using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Models;
using FitnessClub.Services;
using System.Collections.ObjectModel;

namespace FitnessClub.ViewModels;

public partial class ClientsViewModel : BaseViewModel
{
    private readonly DatabaseService _db;

    [ObservableProperty]
    private ObservableCollection<Client> clients = new();

    public ClientsViewModel(DatabaseService db)
    {
        _db = db;
        Title = "Клієнти";
    }

    [RelayCommand]
    private void Load()
    {
        var data = _db.GetClients();
        Clients.Clear();
        foreach (var c in data)
            Clients.Add(c);
    }

    [RelayCommand]
    private async Task AddAsync()
    {
        await Shell.Current.GoToAsync("clientedit?client_id=0");
    }

    [RelayCommand]
    private async Task EditAsync(int id)
    {
        await Shell.Current.GoToAsync($"clientedit?client_id={id}");
    }

    [RelayCommand]
    private async Task DeleteAsync(int id)
    {
        bool confirm = await Shell.Current.DisplayAlert(
            "Видалення", "Видалити цього клієнта?", "Так", "Скасувати");
        if (!confirm) return;

        var item = _db.GetClient(id);
        if (item != null)
        {
            _db.DeleteClient(item);
            Load();
        }
    }
}