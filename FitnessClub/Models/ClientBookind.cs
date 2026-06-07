using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FitnessClub.Models
{
    [Table("ЗаписиКлієнтів")]
    public class ClientBooking : INotifyPropertyChanged
    {
        private DateTime bookingDate = DateTime.Today;

        [PrimaryKey, AutoIncrement]
        [Column("ідентифікатор_запису")]
        public int Id { get; set; }

        [Column("ідентифікатор_клієнта")]
        public int ClientId { get; set; }

        [Column("ідентифікатор_заняття")]
        public int ScheduleId { get; set; }

        [Column("дата_запису")]
        public DateTime BookingDate
        {
            get => bookingDate;
            set { if (bookingDate != value) { bookingDate = value; OnPropertyChanged(); } }
        }

        private string clientName = string.Empty;
        private string scheduleName = string.Empty;
        private string dayOfWeek = string.Empty;

        [Ignore]
        public string ClientName
        {
            get => clientName;
            set { if (clientName != value) { clientName = value; OnPropertyChanged(); } }
        }

        [Ignore]
        public string ScheduleName
        {
            get => scheduleName;
            set { if (scheduleName != value) { scheduleName = value; OnPropertyChanged(); } }
        }

        [Ignore]
        public string DayOfWeek
        {
            get => dayOfWeek;
            set { if (dayOfWeek != value) { dayOfWeek = value; OnPropertyChanged(); } }
        }

        [Ignore]
        public bool IsActive => BookingDate.Date >= DateTime.Today;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}