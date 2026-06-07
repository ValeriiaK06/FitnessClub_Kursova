using FitnessClub.ViewModels;

namespace FitnessClub.Views.Details;

public partial class BookingEditPage : ContentPage
{
    public BookingEditPage(BookingEditViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}