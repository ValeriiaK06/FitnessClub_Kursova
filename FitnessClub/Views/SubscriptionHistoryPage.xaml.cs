using FitnessClub.ViewModels;

namespace FitnessClub.Views;

public partial class SubscriptionHistoryPage : ContentPage
{
    private readonly SubscriptionHistoryViewModel _vm;

    public SubscriptionHistoryPage(SubscriptionHistoryViewModel vm)
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