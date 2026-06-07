using FitnessClub.ViewModels;

namespace FitnessClub.Views.Details;

public partial class SubscriptionEditPage : ContentPage
{
    public SubscriptionEditPage(SubscriptionEditViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}