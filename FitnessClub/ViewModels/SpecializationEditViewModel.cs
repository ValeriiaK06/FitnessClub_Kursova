using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Models;
using FitnessClub.Services;
using System.Xml.Linq;

namespace FitnessClub.ViewModels;

[QueryProperty(nameof(SpecId), "spec_id")]
public partial class SpecializationEditViewModel : BaseViewModel
{
    private readonly DatabaseService _db;
    private readonly INavigationService _nav;
    private readonly IDialogService _dialog;
    private Specialization? _editing;

    [ObservableProperty] private string name = string.Empty;

    public SpecializationEditViewModel(DatabaseService db, INavigationService nav, IDialogService dialog)
    {
        _db = db;
        _nav = nav;
        _dialog = dialog;
    }

    private int _specId;
    public int SpecId
    {
        get => _specId;
        set { _specId = value; Load(); }
    }

    private void Load()
    {
        if (_specId == 0)
        {
            Title = "Нова спеціалізація";
            _editing = null;
            return;
        }

        Title = "Редагувати спеціалізацію";
        _editing = _db.GetSpecialization(_specId);
        if (_editing != null) Name = _editing.Name;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            await _dialog.AlertAsync("Помилка", "Введіть назву спеціалізації", "OK");
            return;
        }

        if (_editing == null)
            _db.AddSpecialization(new Specialization { Name = Name });
        else
        {
            _editing.Name = Name;
            _db.UpdateSpecialization(_editing);
        }

        await _nav.GoBackAsync();
    }

    [RelayCommand]
    private async Task CancelAsync() => await _nav.GoBackAsync();
}