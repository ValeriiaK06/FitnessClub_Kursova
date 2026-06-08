using FitnessClub.ViewModels;

namespace FitnessClub.Views.Details;

public partial class SpecializationEditPage : ContentPage
{
    public SpecializationEditPage(SpecializationEditViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}