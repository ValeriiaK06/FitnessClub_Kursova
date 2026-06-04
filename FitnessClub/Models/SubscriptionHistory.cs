using SQLite;

namespace FitnessClub.Models
{
    [Table("ІсторіяАбонементів")]
    public class SubscriptionHistory
    {


        [MaxLength(300)]
        [Column("деталі_змін")]
        public string Details { get; set; } = string.Empty;


        [PrimaryKey, AutoIncrement]
        [Column("ідентифікатор_запису")]
        public int Id { get; set; }

        [MaxLength(100)]
        [Column("клієнт")]
        public string ClientName { get; set; } = string.Empty;

        [MaxLength(100)]
        [Column("абонемент")]
        public string PlanName { get; set; } = string.Empty;

        [Column("дата_придбання")]
        public DateTime PurchaseDate { get; set; }

        [Column("дата_закінчення")]
        public DateTime ExpiryDate { get; set; }

        [Column("дата_дії")]
        public DateTime ActionDate { get; set; }

        [MaxLength(20)]
        [Column("тип_дії")]
        public string ActionType { get; set; } = string.Empty;
    }
}