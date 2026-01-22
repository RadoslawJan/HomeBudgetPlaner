using HomeBudgetProject.Enums;
using System.Text.Json;

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
                Console.WriteLine("Brak uprawnie� do przegl�dania log�w systemowych!");
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
                List<Log> loadedLogs = JsonSerializer.Deserialize<List<Log>>(logs);  //Informacja na ��to �e mo�e by� null, ale poni�ej sprawdzenie
                if(loadedLogs == null) {  return new List<Log>(); }
                return loadedLogs;
            }
            catch { return new List<Log>(); }
        }
        private void SaveLogs()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true; // dla �adnego wy�wietlania
            options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping; //dodatkowo do utf-8
            string logs = JsonSerializer.Serialize(logEntries, options);
            File.WriteAllText(filePath, logs, System.Text.Encoding.UTF8);
        }
    }
}
