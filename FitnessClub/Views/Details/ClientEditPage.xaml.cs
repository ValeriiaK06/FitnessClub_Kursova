using FitnessClub.ViewModels;

namespace FitnessClub.Views.Details;

public partial class ClientEditPage : ContentPage
{
    public ClientEditPage(ClientEditViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}