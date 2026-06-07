using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FitnessClub.Models
{
    [Table("Тренери")]
    public class Trainer : INotifyPropertyChanged
    {
        private string lastName = string.Empty;
        private string firstName = string.Empty;
        private string middleName = string.Empty;
        private string specialization = string.Empty;
        private int experience;
        private string photo = string.Empty;
        private string phone = string.Empty;
        private string email = string.Empty;

        [PrimaryKey, AutoIncrement]
        [Column("ідентифікатор_тренера")]
        public int Id { get; set; }

        [MaxLength(50)]
        [Column("прізвище")]
        public string LastName
        {
            get => lastName;
            set { if (lastName != value) { lastName = value; OnPropertyChanged(); } }
        }

        [MaxLength(50)]
        [Column("ім'я")]
        public string FirstName
        {
            get => firstName;
            set { if (firstName != value) { firstName = value; OnPropertyChanged(); } }
        }

        [MaxLength(50)]
        [Column("по_батькові")]
        public string MiddleName
        {
            get => middleName;
            set { if (middleName != value) { middleName = value; OnPropertyChanged(); } }
        }

        [MaxLength(100)]
        [Column("спеціалізація")]
        public string Specialization
        {
            get => specialization;
            set { if (specialization != value) { specialization = value; OnPropertyChanged(); } }
        }

        [Column("досвід_роботи")]
        public int Experience
        {
            get => experience;
            set { if (experience != value) { experience = value; OnPropertyChanged(); } }
        }

        [MaxLength(200)]
        [Column("фото")]
        public string Photo
        {
            get => photo;
            set { if (photo != value) { photo = value; OnPropertyChanged(); } }
        }

        [MaxLength(20)]
        [Column("телефон")]
        public string Phone
        {
            get => phone;
            set { if (phone != value) { phone = value; OnPropertyChanged(); } }
        }

        [MaxLength(100)]
        [Column("email")]
        public string Email
        {
            get => email;
            set { if (email != value) { email = value; OnPropertyChanged(); } }
        }

        [Ignore]
        public bool HasPhoto => !string.IsNullOrWhiteSpace(Photo);

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}