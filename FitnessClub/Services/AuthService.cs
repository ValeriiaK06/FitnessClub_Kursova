using System.Security.Cryptography;
using System.Text;
using FitnessClub.Models;

namespace FitnessClub.Services
{
    public class AuthService
    {
        private const string DefaultLogin = "admin";
        private const string DefaultPassword = "admin123";
        private const string DefaultQuestion = "Введіть пароль скидання";
        private const string DefaultAnswer = "saveadmin";

        private readonly DatabaseService _db;

        public AuthService(DatabaseService db)
        {
            _db = db;
            EnsureCreated();
        }

        // Якщо рядка адміна ще немає — створюємо стандартний
        private void EnsureCreated()
        {
            if (_db.GetAdminSettings() == null)
            {
                _db.SaveAdminSettings(new AdminSettings
                {
                    Id = 1,
                    Login = DefaultLogin,
                    PasswordHash = Hash(DefaultPassword),
                    AdminName = "Адміністратор",
                    SecurityQuestion = DefaultQuestion,
                    SecurityAnswerHash = Hash(DefaultAnswer.ToLower().Trim())
                });
            }
        }

        // ===== ВХІД =====
        public bool Validate(string login, string password)
        {
            var s = _db.GetAdminSettings();
            if (s == null) return false;
            return login == s.Login && Hash(password) == s.PasswordHash;
        }

        // ===== ПАРОЛЬ =====
        public bool CheckPassword(string password)
        {
            var s = _db.GetAdminSettings();
            return s != null && Hash(password) == s.PasswordHash;
        }

        public void ChangePassword(string newPassword)
        {
            var s = _db.GetAdminSettings();
            if (s == null) return;
            s.PasswordHash = Hash(newPassword);
            _db.SaveAdminSettings(s);
        }

        // ===== ЛОГІН / ІМ'Я =====
        public string GetLogin() => _db.GetAdminSettings()?.Login ?? DefaultLogin;

        public string GetAdminName() => _db.GetAdminSettings()?.AdminName ?? "Адміністратор";

        public void SetAdminName(string name)
        {
            var s = _db.GetAdminSettings();
            if (s == null) return;
            s.AdminName = name;
            _db.SaveAdminSettings(s);
        }

       
        public string GetSecurityQuestion() =>
            _db.GetAdminSettings()?.SecurityQuestion ?? DefaultQuestion;

        public bool CheckSecurityAnswer(string answer)
        {
            var s = _db.GetAdminSettings();
            if (s == null) return false;
            return Hash(answer.ToLower().Trim()) == s.SecurityAnswerHash;
        }

        public void SetSecurityQuestion(string question, string answer)
        {
            var s = _db.GetAdminSettings();
            if (s == null) return;
            s.SecurityQuestion = question;
            s.SecurityAnswerHash = Hash(answer.ToLower().Trim());
            _db.SaveAdminSettings(s);
        }

      
        private static string Hash(string text)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(text));
            return Convert.ToHexString(bytes);
        }
    }
}