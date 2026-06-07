using System.Globalization;

namespace FitnessClub.Converters
{
    // true (активний) → зелений, false (минув) → червоний
    public class ActiveColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isActive = value is bool b && b;
            return isActive
                ? Color.FromArgb("#10B981")   // зелений (Success)
                : Color.FromArgb("#EF4444");  // червоний (Danger)
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}