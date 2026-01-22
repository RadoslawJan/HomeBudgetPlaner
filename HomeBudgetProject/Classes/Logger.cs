using HomeBudgetProject.Enums;
using System.Text.Json;
using System.IO;

namespace HomeBudgetProject.Classes
{
    internal class Logger
    {
        private string filePath = "system_logs.json";
        private static Logger? _instance;
        private List<Log> logEntries;

        private Logger() {
            logEntries = LoadLogs();
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
            SaveLogs();
        }

        public void GetLogsForAdmin(User currentUser)
        {
            if (currentUser.Status != StatusLevel.Admin)
            {
                Console.WriteLine("Brak uprawnieñ do przegl¹dania logów systemowych!");
                return;
            }

            int number = 1;
            foreach (Log log in logEntries)
            {
                Console.WriteLine($"{number}. {log}");
                number++;
            }
        }
        private List<Log> LoadLogs()
        {
            if (File.Exists(filePath) == false) { return new List<Log>(); }
            try
            {
                string logs = File.ReadAllText(filePath, System.Text.Encoding.UTF8); //domyslnie bez polskichznakow
                List<Log> loadedLogs = JsonSerializer.Deserialize<List<Log>>(logs);  //Informacja na ¿ó³to ¿e mo¿e byæ null, ale poni¿ej sprawdzenie
                if(loadedLogs == null) {  return new List<Log>(); }
                return loadedLogs;
            }
            catch { return new List<Log>(); }
        }
        private void SaveLogs()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true; // dla ³adnego wyœwietlania
            options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping; //dodatkowo do utf-8
            string logs = JsonSerializer.Serialize(logEntries, options);
            File.WriteAllText(filePath, logs, System.Text.Encoding.UTF8);
        }

    }

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

    public enum LogType
    {
        LogIn,
        LogOut,
        SuccessfulOperation,
        FailedOperation
    }
}
