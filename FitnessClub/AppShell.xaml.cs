using FitnessClub.Services;
using FitnessClub.Views;

namespace FitnessClub
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            ThemeSwitch.IsToggled = Application.Current.RequestedTheme == AppTheme.Dark;

            // Маршрути edit-сторінок (зареєструємо по мірі створення розділів)
            //Routing.RegisterRoute("traineredit", typeof(TrainerEditPage));
            //Routing.RegisterRoute("clientedit", typeof(ClientEditPage));
            //Routing.RegisterRoute("subscriptionedit", typeof(SubscriptionEditPage));
            //Routing.RegisterRoute("serviceedit", typeof(ServiceEditPage));
            //Routing.RegisterRoute("scheduleedit", typeof(ScheduleEditPage));
            //Routing.RegisterRoute("clientsubedit", typeof(ClientSubEditPage));
            //Routing.RegisterRoute("bookingedit", typeof(BookingEditPage));
        }

       

        private void OnThemeSwitchToggled(object sender, ToggledEventArgs e)
        {
            Application.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;
        }
    }
}