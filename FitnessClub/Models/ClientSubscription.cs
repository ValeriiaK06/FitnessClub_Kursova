using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FitnessClub.Models
{
    [Table("АбонементиКлієнтів")]
    public class ClientSubscription : INotifyPropertyChanged
    {
        private DateTime purchaseDate = DateTime.Today;
        private DateTime expiryDate = DateTime.Today.AddMonths(1);
        private bool isActive;

        [PrimaryKey, AutoIncrement]
        [Column("ідентифікатор_покупки")]
        public int Id { get; set; }

        [Column("ідентифікатор_клієнта")]
        public int ClientId { get; set; }

        [Column("ідентифікатор_абонементу")]
        public int SubscriptionId { get; set; }

        [Column("дата_придбання")]
        public DateTime PurchaseDate
        {
            get => purchaseDate;
            set { if (purchaseDate != value) { purchaseDate = value; OnPropertyChanged(); } }
        }

        [Column("дата_закінчення")]
        public DateTime ExpiryDate
        {
            get => expiryDate;
            set { if (expiryDate != value) { expiryDate = value; OnPropertyChanged(); } }
        }

        [Column("статус")]
        public bool IsActive
        {
            get => isActive;
            set { if (isActive != value) { isActive = value; OnPropertyChanged(); } }
        }

        private string clientName = string.Empty;
        private string planName = string.Empty;

        // Ім'я клієнта (підвантажується з БД)
        [Ignore]
        public string ClientName
        {
            get => clientName;
            set { if (clientName != value) { clientName = value; OnPropertyChanged(); } }
        }

        // Назва абонемента (підвантажується з БД)
        [Ignore]
        public string PlanName
        {
            get => planName;
            set { if (planName != value) { planName = value; OnPropertyChanged(); } }
        }

        // Чи прострочений — автоматично за датою
        [Ignore]
        public bool IsExpired => ExpiryDate.Date < DateTime.Today;

        // Текст статусу для відображення
        [Ignore]
        public string StatusText => IsExpired ? "Прострочений" : "Активний";
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}