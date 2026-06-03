using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FitnessClub.Models
{
    [Table("Абонементи")]
    public class Subscription : INotifyPropertyChanged
    {
        private string name = string.Empty;
        private string description = string.Empty;
        private double pricePerMonth;

        [PrimaryKey, AutoIncrement]
        [Column("ідентифікатор_абонементу")]
        public int Id { get; set; }

        [MaxLength(100)]
        [Column("назва")]
        public string Name
        {
            get => name;
            set { if (name != value) { name = value; OnPropertyChanged(); } }
        }

        [MaxLength(500)]
        [Column("опис")]
        public string Description
        {
            get => description;
            set { if (description != value) { description = value; OnPropertyChanged(); } }
        }

        [Column("ціна_за_місяць")]
        public double PricePerMonth
        {
            get => pricePerMonth;
            set { if (pricePerMonth != value) { pricePerMonth = value; OnPropertyChanged(); } }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}