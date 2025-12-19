using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudgetProject.Enums;

namespace HomeBudgetProject.Classes
{
    public class User
    {
        public string Nickname;
        public string Password;
        public StatusLevel Status;
        public User(string Nickname, string Password, StatusLevel level) {
            this.Nickname = Nickname;
            this.Password = Password;
            Status = level;
        }
    }
}
