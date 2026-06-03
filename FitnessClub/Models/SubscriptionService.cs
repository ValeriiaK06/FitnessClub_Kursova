using SQLite;

namespace FitnessClub.Models
{
    [Table("ПослугиАбонементів")]
    public class SubscriptionService
    {
        [PrimaryKey, AutoIncrement]
        [Column("ідентифікатор_зв'язку")]
        public int Id { get; set; }

        [Column("ідентифікатор_абонементу")]
        public int SubscriptionId { get; set; }

        [Column("ідентифікатор_послуги")]
        public int ServiceId { get; set; }
    }
}