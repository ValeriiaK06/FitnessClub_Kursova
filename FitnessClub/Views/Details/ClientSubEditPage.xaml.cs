using FitnessClub.ViewModels;

namespace FitnessClub.Views.Details;

public partial class ClientSubEditPage : ContentPage
{
    public ClientSubEditPage(ClientSubEditViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}