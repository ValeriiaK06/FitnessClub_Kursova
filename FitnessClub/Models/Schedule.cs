using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FitnessClub.Models
{
    [Table("РозкладЗанять")]
    public class Schedule : INotifyPropertyChanged
    {
        private string name = string.Empty;
        private string dayOfWeek = string.Empty;
        private TimeSpan startTime;
        private int duration;

        [PrimaryKey, AutoIncrement]
        [Column("ідентифікатор_заняття")]
        public int Id { get; set; }

        [MaxLength(100)]
        [Column("назва")]
        public string Name
        {
            get => name;
            set { if (name != value) { name = value; OnPropertyChanged(); } }
        }

        [MaxLength(20)]
        [Column("день_тижня")]
        public string DayOfWeek
        {
            get => dayOfWeek;
            set { if (dayOfWeek != value) { dayOfWeek = value; OnPropertyChanged(); } }
        }

        [Column("час_початку")]
        public TimeSpan StartTime
        {
            get => startTime;
            set { if (startTime != value) { startTime = value; OnPropertyChanged(); } }
        }

        [Column("тривалість")]
        public int Duration
        {
            get => duration;
            set { if (duration != value) { duration = value; OnPropertyChanged(); } }
        }

        [Column("ідентифікатор_тренера")]
        public int TrainerId { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}