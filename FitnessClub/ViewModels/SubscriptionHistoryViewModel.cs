using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Models;
using FitnessClub.Services;
using System.Collections.ObjectModel;

namespace FitnessClub.ViewModels;

public partial class SubscriptionHistoryViewModel : BaseViewModel
{
    private readonly DatabaseService _db;

    [ObservableProperty]
    private ObservableCollection<SubscriptionHistory> history = new();

    public SubscriptionHistoryViewModel(DatabaseService db)
    {
        _db = db;
        Title = "Історія";
    }

    [RelayCommand]
    private void Load()
    {
        var data = _db.GetSubscriptionHistory();
        History.Clear();
        foreach (var h in data)
            History.Add(h);
    }
}