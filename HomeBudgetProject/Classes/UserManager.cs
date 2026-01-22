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
                users.Add(new User("admin", "admin", StatusLevel.Admin));
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
                string json = File.ReadAllText(FilePath);
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
            File.WriteAllText(FilePath, json);
        }

        public User Authenticate(string nickname, string password)
        {
            foreach (User u in users)
            {
                if (u.Nickname == nickname && u.Password == password)
                {
                    return u;
                }
            }
            return null;
        }

        public bool RegisterUser(string nickname, string password, StatusLevel level, string adminAuthPassword = null)
        {
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

                if (admin == null || admin.Password != adminAuthPassword)
                {
                    return false;
                }
            }

            users.Add(new User(nickname, password, level));
            SaveUsers();
            return true;
        }
    }
}