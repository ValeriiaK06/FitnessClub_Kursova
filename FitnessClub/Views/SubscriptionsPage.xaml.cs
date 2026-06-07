using FitnessClub.ViewModels;

namespace FitnessClub.Views;

public partial class SubscriptionsPage : ContentPage
{
    private readonly SubscriptionsViewModel _vm;

    public SubscriptionsPage(SubscriptionsViewModel vm)
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