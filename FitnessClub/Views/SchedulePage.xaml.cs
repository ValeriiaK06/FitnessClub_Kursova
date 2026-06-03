using FitnessClub.ViewModels;

namespace FitnessClub.Views;

public partial class SchedulePage : ContentPage
{
    private readonly ScheduleViewModel _vm;

    public SchedulePage(ScheduleViewModel vm)
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