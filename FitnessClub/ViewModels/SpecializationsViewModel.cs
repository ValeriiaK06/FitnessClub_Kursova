using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Models;
using FitnessClub.Services;
using System.Collections.ObjectModel;

namespace FitnessClub.ViewModels;

public partial class SpecializationsViewModel : BaseViewModel
{
    private readonly DatabaseService _db;
    private readonly INavigationService _nav;
    private readonly IDialogService _dialog;

    [ObservableProperty]
    private ObservableCollection<Specialization> specializations = new();

    public SpecializationsViewModel(DatabaseService db, INavigationService nav, IDialogService dialog)
    {
        _db = db;
        _nav = nav;
        _dialog = dialog;
        Title = "Спеціалізації";
    }

    [RelayCommand]
    private void Load()
    {
        var data = _db.GetSpecializations();
        Specializations.Clear();
        foreach (var s in data) Specializations.Add(s);
    }

    [RelayCommand]
    private async Task AddAsync() => await _nav.GoToAsync("specializationedit?spec_id=0");

    [RelayCommand]
    private async Task EditAsync(int id) => await _nav.GoToAsync($"specializationedit?spec_id={id}");

    [RelayCommand]
    private async Task DeleteAsync(int id)
    {
        bool confirm = await _dialog.ConfirmAsync(
            "Видалення", "Видалити цю спеціалізацію?", "Так", "Скасувати");
        if (!confirm) return;

        var item = _db.GetSpecialization(id);
        if (item != null) { _db.DeleteSpecialization(item); Load(); }
    }
}