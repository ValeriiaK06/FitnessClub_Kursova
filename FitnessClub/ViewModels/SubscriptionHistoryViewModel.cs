using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Models;
using FitnessClub.Services;
using System.Collections.ObjectModel;

namespace FitnessClub.ViewModels;

public partial class SubscriptionHistoryViewModel : BaseViewModel
{
    private readonly DatabaseService _db;

    // Повний список (джерело для фільтра)
    private List<SubscriptionHistory> _all = new();

    [ObservableProperty]
    private ObservableCollection<SubscriptionHistory> history = new();

    [ObservableProperty]
    private string searchText = string.Empty;

    public SubscriptionHistoryViewModel(DatabaseService db)
    {
        _db = db;
        Title = "Історія";
    }

    [RelayCommand]
    private void Load()
    {
        _all = _db.GetSubscriptionHistory();
        ApplyFilter();
    }

    // Автоматично спрацьовує при зміні тексту пошуку
    partial void OnSearchTextChanged(string value)
    {
        ApplyFilter();
    }

    private void ApplyFilter()
    {
        var query = SearchText?.Trim() ?? string.Empty;

        var filtered = string.IsNullOrEmpty(query)
            ? _all
            : _all.Where(h => h.ClientName
                .Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

        History.Clear();
        foreach (var h in filtered)
            History.Add(h);
    }
}