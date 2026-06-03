using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FitnessClub.Models
{
    [Table("Послуги")]
    public class Service : INotifyPropertyChanged
    {
        private string name = string.Empty;

        [PrimaryKey, AutoIncrement]
        [Column("ідентифікатор_послуги")]
        public int Id { get; set; }

        [MaxLength(100)]
        [Column("назва")]
        public string Name
        {
            get => name;
            set { if (name != value) { name = value; OnPropertyChanged(); } }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}