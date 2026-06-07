namespace FitnessClub.Services
{
    public class NavigationService : INavigationService
    {
        public Task GoToAsync(string route) => Shell.Current.GoToAsync(route);
        public Task GoBackAsync() => Shell.Current.GoToAsync("..");
    }
}