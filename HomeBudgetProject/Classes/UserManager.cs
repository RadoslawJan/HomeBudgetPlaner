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
        private List<User> users;

        public UserManager()
        {
            users = LoadUsers();

            bool adminFound = false;
            foreach (User u in users)
            {
                if (u.Nickname == "admin")
                {
                    adminFound = true;
                    break;
                }
            }

            if (adminFound == false)
            {
                users.Add(new User("admin", HashPaswd("admin"), StatusLevel.Admin));
                SaveUsers();
            }
        }

        private List<User> LoadUsers()
        {
            if (File.Exists(FilePath) == false)
            {
                return new List<User>();
            }

            try
            {
                string json = File.ReadAllText(FilePath, Encoding.UTF8);
                List<User> list = JsonSerializer.Deserialize<List<User>>(json);

                if (list == null)
                {
                    return new List<User>();
                }

                return list;
            }
            catch
            {
                return new List<User>();
            }
        }

        private void SaveUsers()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true; //formatowanie w przystępny sposób

            string json = JsonSerializer.Serialize(users, options);
            File.WriteAllText(FilePath, json, System.Text.Encoding.UTF8);
        }

        public User Authenticate(string nickname, string password)
        {
            string hashedPassword = HashPaswd(password);

            foreach (User u in users)
            {
                if (u.Nickname == nickname && u.Password == hashedPassword)
                {
                    return u;
                }
            }
            return null;
        }

        public bool RegisterUser(string nickname, string password, StatusLevel level, string adminAuthPassword = null)
        {
            string hashedAdminAuthPassword = HashPaswd(adminAuthPassword);
            bool exists = false;
            foreach (User u in users)
            {
                if (u.Nickname == nickname)
                {
                    exists = true;
                    break;
                }
            }

            if (exists == true)
            {
                return false;
            }

            if (level == StatusLevel.VIP || level == StatusLevel.Admin)
            {
                User admin = null;
                foreach (User u in users)
                {
                    if (u.Status == StatusLevel.Admin)
                    {
                        admin = u;
                        break;
                    }
                }

                if (admin == null || admin.Password != hashedAdminAuthPassword)
                {
                    return false;
                }
            }

            users.Add(new User(nickname, HashPaswd(password), level));
            SaveUsers();
            return true;
        }

        private string HashPaswd(string password) 
        {
            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}