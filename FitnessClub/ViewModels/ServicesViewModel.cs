using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Models;
using FitnessClub.Services;
using System.Collections.ObjectModel;

namespace FitnessClub.ViewModels;

public partial class ServicesViewModel : BaseViewModel
{
    private readonly DatabaseService _db;
    private readonly INavigationService _nav;
    private readonly IDialogService _dialog;

    [ObservableProperty]
    private ObservableCollection<Service> services = new();

    public ServicesViewModel(DatabaseService db, INavigationService nav, IDialogService dialog)
    {
        _db = db;
        _nav = nav;
        _dialog = dialog;
        Title = "Послуги";
    }

    [RelayCommand]
    private void Load()
    {
        var data = _db.GetServices();
        Services.Clear();
        foreach (var s in data) Services.Add(s);
    }

    [RelayCommand]
    private async Task AddAsync() => await _nav.GoToAsync("serviceedit?service_id=0");

    [RelayCommand]
    private async Task EditAsync(int id) => await _nav.GoToAsync($"serviceedit?service_id={id}");

    [RelayCommand]
    private async Task DeleteAsync(int id)
    {
        bool confirm = await _dialog.ConfirmAsync("Видалення", "Видалити цю послугу? Вона зникне з усіх абонементів.", "Так", "Скасувати");
        if (!confirm) return;
        var item = _db.GetService(id);
        if (item != null) { _db.DeleteService(item); Load(); }
    }
}