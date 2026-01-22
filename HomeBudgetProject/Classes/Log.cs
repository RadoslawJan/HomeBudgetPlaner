using HomeBudgetProject.Enums;

namespace HomeBudgetProject.Classes
{
    internal class Log
    {
        public LogType logType { get; set; }
        public User user { get; set; }
        public string? description { get; set; }

        public Log(LogType logType, User user, string description)
        {
            this.logType = logType;
            this.user = user;
            this.description = description;
        }

        public override string ToString()
        {
            return $"[{logType}] {user.Nickname} {description}";
        }
    }
}
