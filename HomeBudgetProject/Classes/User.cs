using HomeBudgetProject.Enums;

namespace HomeBudgetProject.Classes
{
    public class User
    {
        public string Nickname { get; set; }
        public string Password { get; set; }
        public StatusLevel Status { get; set; }

        public User(string nickname, string password, StatusLevel level)
        {
            Nickname = nickname;
            Password = password;
            Status = level;
        }
    }
}
