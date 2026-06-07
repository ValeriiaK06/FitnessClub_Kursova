using CommunityToolkit.Mvvm.ComponentModel;

namespace FitnessClub.ViewModels;

public partial class ServiceSelectionItem : ObservableObject
{
    public int ServiceId { get; set; }
    public string Name { get; set; } = string.Empty;

    [ObservableProperty]
    private bool isSelected;
}