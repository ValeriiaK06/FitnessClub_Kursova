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
    private Service? _editing;

    [ObservableProperty] private string name = string.Empty;

    public ServiceEditViewModel(DatabaseService db)
    {
        _db = db;
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
            await Shell.Current.DisplayAlert("Помилка", "Введіть назву послуги", "OK");
            return;
        }

        if (_editing == null)
            _db.AddService(new Service { Name = Name });
        else
        {
            _editing.Name = Name;
            _db.UpdateService(_editing);
        }

        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}