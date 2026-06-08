using SQLite;

namespace FitnessClub.Models
{
    [Table("Адміністратор")]
    public class AdminSettings
    {
        [PrimaryKey]
        [Column("ідентифікатор")]
        public int Id { get; set; } = 1;   // завжди один рядок

        [Column("логін")]
        public string Login { get; set; } = string.Empty;

        [Column("хеш_пароля")]
        public string PasswordHash { get; set; } = string.Empty;

        [Column("імя")]
        public string AdminName { get; set; } = "Адміністратор";


        [Column("секретне_питання")]
        public string SecurityQuestion { get; set; } = string.Empty;

        [Column("хеш_відповіді")]
        public string SecurityAnswerHash { get; set; } = string.Empty;
    }


}