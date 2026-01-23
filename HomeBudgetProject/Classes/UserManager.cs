using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using HomeBudgetProject.Enums;

namespace HomeBudgetProject.Classes
{
    public class UserManager
    {
        private const string FilePath = "users.json";
        private readonly List<User> _users;

        public UserManager()
        {
            _users = LoadUsers();
            EnsureDefaultAdminExists();
        }


        private List<User> LoadUsers()
        {
            if (!File.Exists(FilePath))
            {
                return new List<User>();
            }

            try
            {
                string json = File.ReadAllText(FilePath, Encoding.UTF8);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var list = JsonSerializer.Deserialize<List<User>>(json, options);

                return list!;
            }
            catch
            {
                Console.WriteLine("Błąd");
                return new List<User>();
            }
        }

        private void SaveUsers()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_users, options);
            File.WriteAllText(FilePath, json, Encoding.UTF8);
        }

        private void EnsureDefaultAdminExists()
        {
            if (!_users.Any(u => u.Nickname == "admin"))
            {
                _users.Add(new User("admin", HashPaswd("admin"), StatusLevel.Admin));
                SaveUsers();
            }
        }


        public User? Authenticate(string nickname, string password)
        {
            string hashedPassword = HashPaswd(password);

            return _users.FirstOrDefault(u => u.Nickname == nickname && u.Password == hashedPassword);
        }

        public bool RegisterUser(string nickname, string password, StatusLevel level, string? adminAuthPassword = null)
        {
            if (_users.Any(u => u.Nickname == nickname))
            {
                return false;
            }

            if (level >= StatusLevel.VIP)
            {
                if (string.IsNullOrEmpty(adminAuthPassword))
                {
                    return false;
                }

                string hashedAdminAuth = HashPaswd(adminAuthPassword);

                bool isAdminAuthorized = _users.Any(u => u.Status == StatusLevel.Admin && u.Password == hashedAdminAuth);

                if (!isAdminAuthorized)
                {
                    return false; 
                }
            }

            _users.Add(new User(nickname, HashPaswd(password), level));
            SaveUsers();
            
            return true;
        }


        private string HashPaswd(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}