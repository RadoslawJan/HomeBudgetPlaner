using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HomeBudgetProject.Enums;


namespace HomeBudgetProject.Classes
{
    public class UserManager
    {
        private const string FilePath = "users.json";
        private List<User> _users;

        public UserManager()
        {
            _users = LoadUsers();
            if (!_users.Any(u => u.Nickname == "admin"))
            {
                _users.Add(new User("admin", "admin", StatusLevel.Admin));
                SaveUsers();
            }
        }

        private List<User> LoadUsers()
        {
            if (!File.Exists(FilePath)) return new List<User>();
            try
            {
                string json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            catch { return new List<User>(); }
        }

        private void SaveUsers()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_users, options);
            File.WriteAllText(FilePath, json);
        }

        public User? Authenticate(string nickname, string password)
        {
            return _users.FirstOrDefault(u => u.Nickname == nickname && u.Password == password);
        }

        public bool RegisterUser(string nickname, string password, StatusLevel level, string? adminAuthPassword = null)
        {
            if (_users.Any(u => u.Nickname == nickname)) return false;

            if (level == StatusLevel.VIP || level == StatusLevel.Admin)
            {
                var admin = _users.FirstOrDefault(u => u.Status == StatusLevel.Admin);
                if (admin == null || admin.Password != adminAuthPassword) return false;
            }

            _users.Add(new User(nickname, password, level));
            SaveUsers();
            return true;
        }
    }
}