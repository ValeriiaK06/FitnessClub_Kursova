using FitnessClub.ViewModels;

namespace FitnessClub.Views.Details;

public partial class TrainerEditPage : ContentPage
{
    public TrainerEditPage(TrainerEditViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}