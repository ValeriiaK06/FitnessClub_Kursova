using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FitnessClub.Models
{
    [Table("Клієнти")]
    public class Client : INotifyPropertyChanged
    {
        private string lastName = string.Empty;
        private string firstName = string.Empty;
        private string middleName = string.Empty;
        private DateTime birthDate = DateTime.Today;
        private string phone = string.Empty;
        private string email = string.Empty;
        private DateTime registrationDate = DateTime.Today;

        [PrimaryKey, AutoIncrement]
        [Column("ідентифікатор_клієнта")]
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

        [Column("дата_народження")]
        public DateTime BirthDate
        {
            get => birthDate;
            set { if (birthDate != value) { birthDate = value; OnPropertyChanged(); } }
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

        [Column("дата_реєстрації")]
        public DateTime RegistrationDate
        {
            get => registrationDate;
            set { if (registrationDate != value) { registrationDate = value; OnPropertyChanged(); } }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}