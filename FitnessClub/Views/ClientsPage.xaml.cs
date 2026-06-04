using FitnessClub.ViewModels;

namespace FitnessClub.Views;

public partial class ClientsPage : ContentPage
{
    private readonly ClientsViewModel _vm;

    public ClientsPage(ClientsViewModel vm)
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