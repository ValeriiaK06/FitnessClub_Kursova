using FitnessClub.ViewModels;

namespace FitnessClub.Views;

public partial class TrainersPage : ContentPage
{
    private readonly TrainersViewModel _vm;

    public TrainersPage(TrainersViewModel vm)
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