using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Models;
using FitnessClub.Services;
using System.Collections.ObjectModel;

namespace FitnessClub.ViewModels;

public partial class ClientSubsViewModel : BaseViewModel
{
    private readonly DatabaseService _db;
    private readonly INavigationService _nav;
    private readonly IDialogService _dialog;

    [ObservableProperty]
    private ObservableCollection<ClientSubscription> items = new();

    public ClientSubsViewModel(DatabaseService db, INavigationService nav, IDialogService dialog)
    {
        _db = db;
        _nav = nav;
        _dialog = dialog;
        Title = "Абонементи клієнтів";
    }

    [RelayCommand]
    private void Load()
    {
        var data = _db.GetClientSubscriptions();
        Items.Clear();
        foreach (var x in data) Items.Add(x);
    }

    [RelayCommand]
    private async Task AddAsync() => await _nav.GoToAsync("clientsubedit?clientsub_id=0");

    [RelayCommand]
    private async Task EditAsync(int id) => await _nav.GoToAsync($"clientsubedit?clientsub_id={id}");

    
    [RelayCommand]
    private async Task DeleteAsync(int id)
    {
        bool confirm = await _dialog.ConfirmAsync(
            "Видалення", "Видалити цей абонемент клієнта?", "Так", "Скасувати");
        if (!confirm) return;

        var item = _db.GetClientSubscription(id);
        if (item != null)
        {
            
            var all = _db.GetClientSubscriptions();
            var full = all.FirstOrDefault(cs => cs.Id == id);

            _db.DeleteClientSubscription(item);

            _db.AddHistory(new SubscriptionHistory
            {
                ClientName = full?.ClientName ?? "—",
                PlanName = full?.PlanName ?? "—",
                PurchaseDate = item.PurchaseDate,
                ExpiryDate = item.ExpiryDate,
                ActionDate = DateTime.Now,
                ActionType = "Видалено",
                Details = $"Видалено абонемент «{full?.PlanName ?? "—"}» клієнта {full?.ClientName ?? "—"}"
            });

            Load();
        }
    }
}