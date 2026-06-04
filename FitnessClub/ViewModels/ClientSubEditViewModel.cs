using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Models;
using FitnessClub.Services;
using System.Collections.ObjectModel;

namespace FitnessClub.ViewModels;

[QueryProperty(nameof(ClientSubId), "clientsub_id")]
public partial class ClientSubEditViewModel : BaseViewModel
{
    private readonly DatabaseService _db;
    private ClientSubscription? _editing;
    private List<Client> _clients = new();
    private List<Subscription> _subs = new();

    [ObservableProperty] private ObservableCollection<string> clientNames = new();
    [ObservableProperty] private ObservableCollection<string> planNames = new();
    [ObservableProperty] private int selectedClientIndex = -1;
    [ObservableProperty] private int selectedPlanIndex = -1;
    [ObservableProperty] private DateTime purchaseDate = DateTime.Today;
    [ObservableProperty] private DateTime expiryDate = DateTime.Today.AddMonths(1);

    public ClientSubEditViewModel(DatabaseService db)
    {
        _db = db;
    }

    private int _clientSubId;
    public int ClientSubId
    {
        get => _clientSubId;
        set { _clientSubId = value; Load(); }
    }

    private void Load()
    {
        _clients = _db.GetClients();
        _subs = _db.GetSubscriptions();

        ClientNames = new ObservableCollection<string>(
            _clients.Select(c => $"{c.LastName} {c.FirstName}"));
        PlanNames = new ObservableCollection<string>(
            _subs.Select(s => s.Name));

        if (_clientSubId == 0)
        {
            Title = "Новий абонемент";
            _editing = null;
            PurchaseDate = DateTime.Today;
            ExpiryDate = DateTime.Today.AddMonths(1);
            return;
        }

        Title = "Редагувати абонемент";
        _editing = _db.GetClientSubscription(_clientSubId);

        if (_editing != null)
        {
            SelectedClientIndex = _clients.FindIndex(c => c.Id == _editing.ClientId);
            SelectedPlanIndex = _subs.FindIndex(s => s.Id == _editing.SubscriptionId);
            PurchaseDate = _editing.PurchaseDate;
            ExpiryDate = _editing.ExpiryDate;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (SelectedClientIndex < 0)
        {
            await Shell.Current.DisplayAlert("Помилка", "Оберіть клієнта", "OK");
            return;
        }
        if (SelectedPlanIndex < 0)
        {
            await Shell.Current.DisplayAlert("Помилка", "Оберіть абонемент", "OK");
            return;
        }

        int clientId = _clients[SelectedClientIndex].Id;
        int subId = _subs[SelectedPlanIndex].Id;
        bool active = ExpiryDate.Date >= DateTime.Today;

        // Перевірка: один клієнт — один абонемент
        var existing = _db.GetClientSubscriptions()
            .FirstOrDefault(cs => cs.ClientId == clientId);

        if (existing != null && (_editing == null || existing.Id != _editing.Id))
        {
            await Shell.Current.DisplayAlert(
                "Помилка",
                "У цього клієнта вже є абонемент. Один клієнт може мати лише один абонемент — відредагуйте наявний.",
                "OK");
            return;
        }

        string clientName = $"{_clients[SelectedClientIndex].LastName} {_clients[SelectedClientIndex].FirstName}";
        string planName = _subs[SelectedPlanIndex].Name;
        string actionType;
        string details;

        if (_editing == null)
        {
            _db.AddClientSubscription(new ClientSubscription
            {
                ClientId = clientId,
                SubscriptionId = subId,
                PurchaseDate = PurchaseDate,
                ExpiryDate = ExpiryDate,
                IsActive = active
            });
            actionType = "Додано";
            details = $"Створено абонемент «{planName}» до {ExpiryDate:dd.MM.yyyy}";
        }
        else
        {
            // Запам'ятовуємо старі значення ДО перезапису
            var changes = new List<string>();

            if (_editing.SubscriptionId != subId)
            {
                var oldPlan = _subs.FirstOrDefault(s => s.Id == _editing.SubscriptionId)?.Name ?? "—";
                changes.Add($"абонемент: {oldPlan} → {planName}");
            }
            if (_editing.ExpiryDate.Date != ExpiryDate.Date)
                changes.Add($"діє до: {_editing.ExpiryDate:dd.MM.yyyy} → {ExpiryDate:dd.MM.yyyy}");
            if (_editing.PurchaseDate.Date != PurchaseDate.Date)
                changes.Add($"придбано: {_editing.PurchaseDate:dd.MM.yyyy} → {PurchaseDate:dd.MM.yyyy}");

            // Застосовуємо зміни
            _editing.ClientId = clientId;
            _editing.SubscriptionId = subId;
            _editing.PurchaseDate = PurchaseDate;
            _editing.ExpiryDate = ExpiryDate;
            _editing.IsActive = active;
            _db.UpdateClientSubscription(_editing);

            actionType = "Оновлено";
            details = changes.Count > 0
                ? string.Join("; ", changes)
                : "Без змін у даних";
        }

        // Запис у історію
        _db.AddHistory(new SubscriptionHistory
        {
            ClientName = clientName,
            PlanName = planName,
            PurchaseDate = PurchaseDate,
            ExpiryDate = ExpiryDate,
            ActionDate = DateTime.Now,
            ActionType = actionType,
            Details = details
        });

        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}