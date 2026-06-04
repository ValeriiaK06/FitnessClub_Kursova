using System.Globalization;

namespace FitnessClub.Converters
{
    // true (прострочений) → червоний, false (активний) → зелений
    public class StatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isExpired = value is bool b && b;

            // Беремо кольори з ресурсів
            var key = isExpired ? "Danger" : "Success";
            if (Application.Current?.Resources.TryGetValue(key, out var color) == true)
                return color;

            return isExpired ? Colors.Red : Colors.Green;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}