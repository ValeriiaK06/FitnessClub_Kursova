using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Models;
using FitnessClub.Services;
using System.Collections.ObjectModel;

namespace FitnessClub.ViewModels;

public partial class SubscriptionsViewModel : BaseViewModel
{
    private readonly DatabaseService _db;
    private readonly INavigationService _nav;
    private readonly IDialogService _dialog;

    [ObservableProperty]
    private ObservableCollection<Subscription> subscriptions = new();

    public SubscriptionsViewModel(DatabaseService db, INavigationService nav, IDialogService dialog)
    {
        _db = db;
        _nav = nav;
        _dialog = dialog;
        Title = "Абонементи";
    }

    [RelayCommand]
    private void Load()
    {
        var data = _db.GetSubscriptions();
        Subscriptions.Clear();
        foreach (var s in data) Subscriptions.Add(s);
    }

    [RelayCommand]
    private async Task AddAsync() => await _nav.GoToAsync("subscriptionedit?subscription_id=0");

    [RelayCommand]
    private async Task EditAsync(int id) => await _nav.GoToAsync($"subscriptionedit?subscription_id={id}");

    [RelayCommand]
    private async Task DeleteAsync(int id)
    {
        bool confirm = await _dialog.ConfirmAsync("Видалення", "Видалити цей абонемент?", "Так", "Скасувати");
        if (!confirm) return;
        var item = _db.GetSubscription(id);
        if (item != null) { _db.DeleteSubscription(item); Load(); }
    }
}