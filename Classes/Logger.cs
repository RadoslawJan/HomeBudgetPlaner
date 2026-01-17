namespace HomeBudgetProject.Classes
{
    internal class Logger
    {
        private static Logger? _instance;
        private List<Log> logEntries;

        private Logger() {
            logEntries = new List<Log>();
        }

        public static Logger GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Logger();
            }

            return _instance;
        }
        public void Log(LogType logType, User user, string description)
        {
            logEntries.Add(new Log(logType, user, description));
        }

        public void GetLogsForAdmin()
        {
            int number = 1;
            foreach (Log log in logEntries)
            {
                Console.WriteLine($"{number}. {log}");
                number++;
            }
        }
    }

    internal class Log
    {
        public LogType logType;
        public User user;
        public string? description;

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

    public enum LogType
    {
        LogIn,
        LogOut,
        SuccessfulOperation,
        FailedOperation
    }
}
