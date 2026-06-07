using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Models;
using FitnessClub.Services;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace FitnessClub.ViewModels;

[QueryProperty(nameof(SubscriptionId), "subscription_id")]
public partial class SubscriptionEditViewModel : BaseViewModel
{
    private readonly DatabaseService _db;
    private Subscription? _editing;

    [ObservableProperty] private string name = string.Empty;
    [ObservableProperty] private string description = string.Empty;
    [ObservableProperty] private string price = string.Empty;

    [ObservableProperty]
    private ObservableCollection<ServiceSelectionItem> services = new();

    public SubscriptionEditViewModel(DatabaseService db)
    {
        _db = db;
    }

    private int _subscriptionId;
    public int SubscriptionId
    {
        get => _subscriptionId;
        set { _subscriptionId = value; Load(); }
    }

    private void Load()
    {
        // Завантажуємо всі послуги як список з прапорцями
        var allServices = _db.GetServices();
        var selectedIds = _subscriptionId == 0
            ? new List<int>()
            : _db.GetServiceIdsForSubscription(_subscriptionId);

        Services = new ObservableCollection<ServiceSelectionItem>(
            allServices.Select(s => new ServiceSelectionItem
            {
                ServiceId = s.Id,
                Name = s.Name,
                IsSelected = selectedIds.Contains(s.Id)
            }));

        if (_subscriptionId == 0)
        {
            Title = "Новий абонемент";
            _editing = null;
            return;
        }

        Title = "Редагувати абонемент";
        _editing = _db.GetSubscription(_subscriptionId);

        if (_editing != null)
        {
            Name = _editing.Name;
            Description = _editing.Description;
            Price = _editing.PricePerMonth.ToString();
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            await Shell.Current.DisplayAlert("Помилка", "Введіть назву абонемента", "OK");
            return;
        }

        double.TryParse(Price, out double priceValue);

        int subId;
        if (_editing == null)
        {
            var newSub = new Subscription
            {
                Name = Name,
                Description = Description ?? "",
                PricePerMonth = priceValue
            };
            _db.AddSubscription(newSub);
            subId = newSub.Id;
        }
        else
        {
            _editing.Name = Name;
            _editing.Description = Description ?? "";
            _editing.PricePerMonth = priceValue;
            _db.UpdateSubscription(_editing);
            subId = _editing.Id;
        }

        // Зберігаємо обрані послуги
        var selectedIds = Services.Where(s => s.IsSelected).Select(s => s.ServiceId).ToList();
        _db.SetSubscriptionServices(subId, selectedIds);

        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}