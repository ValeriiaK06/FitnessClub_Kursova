using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Models;
using FitnessClub.Services;
using System.Collections.ObjectModel;

namespace FitnessClub.ViewModels;

public partial class ClientsViewModel : BaseViewModel
{
    private readonly DatabaseService _db;
    private readonly INavigationService _nav;
    private readonly IDialogService _dialog;

    [ObservableProperty]
    private ObservableCollection<Client> clients = new();

    public ClientsViewModel(DatabaseService db, INavigationService nav, IDialogService dialog)
    {
        _db = db;
        _nav = nav;
        _dialog = dialog;
        Title = "Клієнти";
    }

    [RelayCommand]
    private void Load()
    {
        var data = _db.GetClients();
        Clients.Clear();
        foreach (var c in data) Clients.Add(c);
    }

    [RelayCommand]
    private async Task AddAsync() => await _nav.GoToAsync("clientedit?client_id=0");

    [RelayCommand]
    private async Task EditAsync(int id) => await _nav.GoToAsync($"clientedit?client_id={id}");

    [RelayCommand]
    private async Task DeleteAsync(int id)
    {
        bool confirm = await _dialog.ConfirmAsync("Видалення", "Видалити цього клієнта?", "Так", "Скасувати");
        if (!confirm) return;
        var item = _db.GetClient(id);
        if (item != null) { _db.DeleteClient(item); Load(); }
    }
}