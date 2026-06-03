using FitnessClub.ViewModels;

namespace FitnessClub.Views.Details;

public partial class ScheduleEditPage : ContentPage
{
    public ScheduleEditPage(ScheduleEditViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}