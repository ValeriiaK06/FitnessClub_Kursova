using FitnessClub.ViewModels;

namespace FitnessClub.Views.Details;

public partial class ServiceEditPage : ContentPage
{
    public ServiceEditPage(ServiceEditViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}