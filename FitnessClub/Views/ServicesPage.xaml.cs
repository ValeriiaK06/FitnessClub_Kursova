using FitnessClub.ViewModels;

namespace FitnessClub.Views;

public partial class ServicesPage : ContentPage
{
    private readonly ServicesViewModel _vm;

    public ServicesPage(ServicesViewModel vm)
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