using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessClub.Models;
using FitnessClub.Services;
using System.Xml.Linq;

namespace FitnessClub.ViewModels;

[QueryProperty(nameof(ServiceId), "service_id")]
public partial class ServiceEditViewModel : BaseViewModel
{
    private readonly DatabaseService _db;
    private readonly INavigationService _nav;
    private readonly IDialogService _dialog;
    private Service? _editing;


    [ObservableProperty] private string name = string.Empty;

    public ServiceEditViewModel(DatabaseService db, INavigationService nav, IDialogService dialog)
    {
        _db = db;
        _nav = nav;
        _dialog = dialog;
    }

    private int _serviceId;
    public int ServiceId
    {
        get => _serviceId;
        set { _serviceId = value; Load(); }
    }

    private void Load()
    {
        if (_serviceId == 0)
        {
            Title = "Нова послуга";
            _editing = null;
            return;
        }

        Title = "Редагувати послугу";
        _editing = _db.GetService(_serviceId);

        if (_editing != null)
            Name = _editing.Name;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            await _dialog.AlertAsync("Помилка", "Введіть назву послуги", "OK");
            return;
        }

        if (_editing == null)
            _db.AddService(new Service { Name = Name });
        else
        {
            _editing.Name = Name;
            _db.UpdateService(_editing);
        }

        await _nav.GoBackAsync();
    }

    [RelayCommand]
    private async Task CancelAsync() => await _nav.GoBackAsync();
}