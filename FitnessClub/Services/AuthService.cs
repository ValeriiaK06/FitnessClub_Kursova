using System.Security.Cryptography;
using System.Text;
using FitnessClub.Models;

namespace FitnessClub.Services
{
    public class AuthService
    {
        private const string DefaultLogin = "admin";
        private const string DefaultPassword = "admin123";

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
                    AdminName = "Адміністратор"
                });
            }
        }

        public bool Validate(string login, string password)
        {
            var s = _db.GetAdminSettings();
            if (s == null) return false;
            return login == s.Login && Hash(password) == s.PasswordHash;
        }

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

        public string GetLogin() => _db.GetAdminSettings()?.Login ?? DefaultLogin;

        public string GetAdminName() => _db.GetAdminSettings()?.AdminName ?? "Адміністратор";

        public void SetAdminName(string name)
        {
            var s = _db.GetAdminSettings();
            if (s == null) return;
            s.AdminName = name;
            _db.SaveAdminSettings(s);
        }

        private static string Hash(string text)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(text));
            return Convert.ToHexString(bytes);
        }
    }
}