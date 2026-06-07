using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Models;
using FitnessClub.Services;
using System.Collections.ObjectModel;

namespace FitnessClub.ViewModels;

public partial class BookingsViewModel : BaseViewModel
{
    private readonly DatabaseService _db;

    // Повний список (джерело для фільтра)
    private List<ClientBooking> _all = new();

    [ObservableProperty]
    private ObservableCollection<ClientBooking> bookings = new();

    [ObservableProperty]
    private string searchText = string.Empty;

    public BookingsViewModel(DatabaseService db)
    {
        _db = db;
        Title = "Записи клієнтів";
    }

    [RelayCommand]
    private void Load()
    {
        _all = _db.GetBookings()
            .OrderByDescending(b => b.BookingDate)
            .ToList();
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
            : _all.Where(b => b.ClientName
                .Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

        Bookings.Clear();
        foreach (var b in filtered)
            Bookings.Add(b);
    }

    [RelayCommand]
    private async Task AddAsync()
    {
        await Shell.Current.GoToAsync("bookingedit?booking_id=0");
    }

    [RelayCommand]
    private async Task EditAsync(int id)
    {
        await Shell.Current.GoToAsync($"bookingedit?booking_id={id}");
    }

    [RelayCommand]
    private async Task DeleteAsync(int id)
    {
        bool confirm = await Shell.Current.DisplayAlert(
            "Видалення", "Видалити цей запис?", "Так", "Скасувати");
        if (!confirm) return;

        var item = _db.GetBooking(id);
        if (item != null)
        {
            _db.DeleteBooking(item);
            Load();
        }
    }
}