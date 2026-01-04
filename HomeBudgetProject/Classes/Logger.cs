using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudgetProject.Classes
{
    internal class Logger
    {
        private static Logger _instance;
        private List<string> logEntries;

        private Logger() {
            logEntries = new List<string>();
        }

        public static Logger GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Logger();
            }

            return _instance;
        }
        public void Log(string message)
        {
            logEntries.Add(message);
        }

        public List<string> GetLogsForAdmin()
        {
            return new List<string>(logEntries);
        }
    }
}
