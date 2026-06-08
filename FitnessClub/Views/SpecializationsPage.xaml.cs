using FitnessClub.ViewModels;

namespace FitnessClub.Views;

public partial class SpecializationsPage : ContentPage
{
    private readonly SpecializationsViewModel _vm;

    public SpecializationsPage(SpecializationsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.LoadCommand.Execute(null);
    }
}